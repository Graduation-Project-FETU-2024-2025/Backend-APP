using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Order_Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders(int pageSize = 5, int pageIndex = 1)
        {
            var orders = await _orderService.GetOrdersAsync(pageSize, pageIndex);
            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                message = "Orders retrieved successfully",
                data = orders
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(new
                {
                    StatusCode = HttpStatusCode.NotFound,
                    message = "Order not found"
                });
            }
            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                message = "Orders retrieved successfully",
                data = order
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderService.CreateOrderAsync(orderDto);
            if(result is null || !result.Succeded)
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = result?.Message ?? "Error Occured",
                    data = result?.Data ?? null
                });
            return CreatedAtAction(nameof(GetById), new {id = result.Data.Id} , new
            {
                StatusCode = HttpStatusCode.Created,
                message = "Order created successfully",
                data = result.Data
            });
        }
    }
}
