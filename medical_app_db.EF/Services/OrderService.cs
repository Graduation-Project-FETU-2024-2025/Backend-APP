using AutoMapper;
using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Order_Module;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public async Task<IReadOnlyList<OrderToReturnDTO>?> GetNotDeliveredOrders(int pageSize = 5, int pageIndex = 1)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;

            var orders = await _context.Set<Order>()
                .Where(o => o.UserEmail == user.Email && o.Status == OrderStatus.Pending)
                .Include(o => o.Branch)
                .Include(o => o.OrderItems)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
        }
        public async Task<IReadOnlyList<OrderToReturnDTO>?> GetUserOrders(int pageSize = 5, int pageIndex = 1)
        {
            _ = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;
            var orders = await _context.Set<Order>()
                .Where(o => o.UserEmail == user.Email)
                .Include(o => o.Branch)
                .Include(o => o.OrderItems)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
        }
        public async Task<IReadOnlyList<OrderToReturnDTO>?> GetBranchOrdersAsync(Guid branchId, int pageSize = 5, int pageIndex = 1)
        {
            var orders = await _context.Set<Order>()
                .Where(o => o.BranchId == branchId)
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
        public async Task<OrderServiceResult?> UpdateOrderAsync(Guid id, OrderDTO orderDto)
        {
            var userId = Guid.Parse(
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User Not Found"
                };
            }

            var branch = await _context.Set<Branch>().FirstOrDefaultAsync(b => b.Id == orderDto.BranchId);
            if (branch == null)
            {
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Branch Not Found"
                };
            }

            var order = await _context.Set<Order>()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Order Not Found"
                };
            }

            if (order.Status != OrderStatus.Pending)
            {
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Order cannot be updated unless it's Pending."
                };
            }

            // Restore stock from old order items
            foreach (var oldItem in order.OrderItems)
            {
                var oldProduct = await _context.Set<BranchProduct>()
                    .FirstOrDefaultAsync(p => p.SystemProductCode == oldItem.SystemProductCode
                                              && p.BranchId == branch.Id);
                if (oldProduct != null)
                {
                    oldProduct.stock += oldItem.Quantity;
                }
            }

            // Remove old order items
            _context.Set<OrderItem>().RemoveRange(order.OrderItems);
            order.OrderItems.Clear();

            order.TotalPrice = 0;

            // Add new order items
            foreach (var itemDto in orderDto.OrderItems)
            {
                var product = await _context.Set<BranchProduct>()
                    .Include(bp => bp.SystemProduct)
                    .FirstOrDefaultAsync(p => p.SystemProductCode == itemDto.SystemProductCode && p.BranchId == branch.Id);

                if (product == null)
                {
                    return new OrderServiceResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Product [{itemDto.SystemProductCode}] Not Found"
                    };
                }

                if (product.stock < itemDto.Quantity)
                {
                    return new OrderServiceResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Insufficient stock for [{product.SystemProduct.EN_Name}]"
                    };
                }

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    SystemProductCode = product.SystemProductCode,
                    Quantity = itemDto.Quantity,
                    SystemProductName = product.SystemProduct.EN_Name,
                    SystemProductImage = product.SystemProduct.Image,
                    SystemProductPrice = (decimal)product.price
                };

                order.OrderItems.Add(orderItem);
                product.stock -= itemDto.Quantity;
                order.TotalPrice += orderItem.Quantity * orderItem.SystemProductPrice;
            }

            // Update delivery price
            var distanceInKilo = GetDistance(orderDto.UserLat, orderDto.UserLong, branch.Lat, branch.Long);
            order.DeliveryPrice = (decimal)distanceInKilo * branch.PricePerKilo;
            order.TotalPrice += order.DeliveryPrice;

            // Update basic order info
            order.BranchId = branch.Id;
            order.Name = user.Name;
            order.UserEmail = orderDto.UserEmail;
            order.UserAddress = orderDto.UserAddress;

            try
            {
                await _context.SaveChangesAsync();

                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Order Updated Successfully",
                    Succeded = true,
                    Data = _mapper.Map<OrderToReturnDTO>(order)
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Order)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = await entry.GetDatabaseValuesAsync();
                        var originalValues = entry.OriginalValues;

                        if (databaseValues == null)
                        {
                            // Entity was deleted by another user
                            _context.Entry(entry.Entity).State = EntityState.Detached;
                            return new OrderServiceResult
                            {
                                StatusCode = HttpStatusCode.NotFound,
                                Message = "Order was deleted by another user",
                                Succeded = false
                            };
                        }

                        // Resolve conflicts property by property
                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];
                            var originalValue = originalValues[property];

                            // Conflict resolution logic
                            if (!Equals(originalValue, databaseValue))
                            {
                                // Database value changed, decide how to resolve
                                if (property.Name == "LastModified" || property.Name == "Version")
                                {
                                    proposedValues[property] = databaseValue;
                                }
                                // Otherwise keep the proposed value
                            }
                        }

                        // Update original values to match database
                        entry.OriginalValues.SetValues(databaseValues);

                        // Try saving again
                        await _context.SaveChangesAsync();
                        return new OrderServiceResult
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Order Updated Successfully",
                            Succeded = true,
                            Data = _mapper.Map<OrderToReturnDTO>(order)
                        };
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Some Error Occurred: {ex.Message}",
                    Data = null
                };
            }

        }

        public async Task<OrderServiceResult?> DeleteOrderAsync(Guid id)
        {
            var order = await _context.Set<Order>().Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Order Not Found",
                    Data = null
                };
            if (order.Status != OrderStatus.Pending)
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Order Cannot Be Canceled, Only Pending Orders Can Be Canceled",
                    Data = null
                };
            var branch = await _context.Set<Branch>()
               .FirstOrDefaultAsync(b => b.Id == order.BranchId);
            if (branch == null)
                return new OrderServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Branch Not Found",
                    Data = null
                };
            if(order.OrderItems != null) 
                foreach (var item in order.OrderItems)
                {
                    var product = await _context.Set<BranchProduct>()
                    .Include(bp => bp.SystemProduct)
                    .FirstOrDefaultAsync(p => p.SystemProductCode == item.SystemProductCode && p.BranchId == branch.Id);

                    product.stock += item.Quantity;
                    _context.Set<BranchProduct>().Update(product);
                }
            _context.Set<Order>().Remove(order);
            await _context.SaveChangesAsync();
            return new OrderServiceResult
            {
                StatusCode = HttpStatusCode.NoContent,
                Message = "Order Deleted Successfully",
                Data = null,
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
