using AutoMapper;
using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Order_Module;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.EF.Services
{
    public class OrderService : IOrderService
    {
        private readonly MedicalDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(MedicalDbContext context, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyList<OrderToReturnDTO>> GetOrdersAsync(int pageSize = 5, int pageIndex = 1)
        {
            var orders = await _context.Set<Order>()
                .Include(o => o.Branch)
                .Include(o => o.OrderItems)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
        }
        public async Task<OrderToReturnDTO?> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Set<Order>()
                .Include(o => o.Branch)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return null;
            return _mapper.Map<OrderToReturnDTO>(order);
        }
        public async Task<OrderServiceResult?> CreateOrderAsync(OrderDTO orderDto)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User Not Found",
                    Data = null
                };
            var branch = await _context.Set<Branch>()
                .FirstOrDefaultAsync(b => b.Id == orderDto.BranchId);
            if(branch == null)
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Branch Not Found",
                    Data = null
                };

            var distanceInKilo = GetDistance(orderDto.UserLat, orderDto.UserLong, branch.Lat, branch.Long);
            decimal deliveryPrice = (decimal)distanceInKilo * branch.PricePerKilo;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OredrDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Name = user.Name,
                UserEmail = user.Email ?? "",
                UserAddress = orderDto.UserAddress,
                BranchId = branch.Id,
                DeliveryPrice = deliveryPrice,
                OrderItems = [],
                TotalPrice = 0,
            };



            foreach (var item in orderDto.OrderItems)
            {
                var product = await _context.Set<BranchProduct>()
                    .Include(bp => bp.SystemProduct)
                    .FirstOrDefaultAsync(p => p.SystemProductCode == item.SystemProductCode && p.BranchId == branch.Id);
                if (product == null)
                    return new OrderServiceResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Product With Code [{item.SystemProductCode}] Not Found",
                        Data = null
                    };
                if (product.stock < item.Quantity)
                    return new OrderServiceResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"No Sufficient Ammount of Product With Name [{product.SystemProduct.EN_Name}]",
                        Data = null
                    };
                var orderItem = new OrderItem
                {
                    SystemProductCode = product.SystemProductCode,
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                    SystemProductName = product.SystemProduct.EN_Name,
                    SystemProductImage = product.SystemProduct.Image,
                    SystemProductPrice = (decimal)product.price,
                    Id = Guid.NewGuid(),
                };

                order.TotalPrice += (decimal)product.price * item.Quantity;
                order.OrderItems.Add(orderItem);
                product.stock -= item.Quantity;
                _context.Set<BranchProduct>().Update(product);
            }

            order.TotalPrice += order.DeliveryPrice;

            try
            {

                var addResult = await _context.Set<Order>().AddAsync(order);
                var saveResult = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                foreach (var item in order.OrderItems)
                {
                    var product = await _context.Set<BranchProduct>()
                    .Include(bp => bp.SystemProduct)
                    .FirstOrDefaultAsync(p => p.SystemProductCode == item.SystemProductCode && p.BranchId == branch.Id);
                    
                    product.stock += item.Quantity;
                }
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Some Error Occured: {ex.Message}",
                    Data = null
                };
            }

            return new OrderServiceResult
            {
                StatusCode = HttpStatusCode.Created,
                Message = $"Some Error Occured",
                Data = _mapper.Map<OrderToReturnDTO>(order) ,
                Succeded = true
            };
        }
        private static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radius of Earth in km
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return distance;
        }
        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }
    }
}
