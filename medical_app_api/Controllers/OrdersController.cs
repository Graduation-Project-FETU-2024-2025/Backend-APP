using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Order_Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
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
            var orders = await _orderService.GetUserOrders(pageSize, pageIndex);
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
        [HttpGet("not-delivered")]
        public async Task<IActionResult> GetNotDeliveredOrders(int pageSize = 5, int pageIndex = 1)
        {
            var orders = await _orderService.GetNotDeliveredOrders(pageSize, pageIndex);
            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                message = "Not delivered orders retrieved successfully",
                data = orders
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            if(id == Guid.Empty)
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = "Invalid order ID",
                });
            var result = await _orderService.DeleteOrderAsync(id);
            if (result is null || !result.Succeded)
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = result?.Message ?? "Error Occured",
                    data = result?.Data ?? null
                });
            return Ok(new
            {
                StatusCode = HttpStatusCode.OK,
                message = "Order deleted successfully",
                data = result.Data
            });
        }
    }
}
