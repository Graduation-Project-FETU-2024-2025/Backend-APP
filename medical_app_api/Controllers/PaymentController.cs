using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymobService;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentService paymentService,IOrderService orderService)
        {
            _paymobService = paymentService;
            _orderService = orderService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartPayment(PaymentRequest paymentRequest)
        {
            var iframeUrl = await _paymobService.GetPayemntUrlAsync(paymentRequest);
            return Ok(new { iframeUrl });
        }
        [HttpPost("callback")]
        public async Task<IActionResult> PaymobCallback()
        {
            var hmacReceived = HttpContext.Request.Query["hmac"].ToString();
            using var reader = new StreamReader(Request.Body);
            var payload = await reader.ReadToEndAsync();

            Console.WriteLine(payload);
            if (payload == null)
                return BadRequest("Invalid payload");

            var paymentData = await _paymobService.ProcessPaymentCallbackAsync(payload, hmacReceived);
            Console.WriteLine(paymentData);
            if (paymentData == null)
            {
                return BadRequest("Invalid callback data");
            }

            if(!paymentData.Success)
            {
                return BadRequest("Payment failed");
            }

            var result = await _orderService.MarkAsPaid(paymentData.merchantOrderId);
            if(result is not null && !result.Succeded)
            {
                return BadRequest("Failed to update order status");
            }
            Console.WriteLine(result);
            return Ok(new
            {
                Message = "Payment processed successfully",
                OrderId = paymentData.merchantOrderId,
            });
        }
        [HttpGet("callback")]
        public IActionResult GetCallbackInfo()
        {
            return Ok(new 
            { 
                Message = "This endpoint only accepts POST requests with payment callback data.",
                StatusCode = HttpStatusCode.OK
            });
        }
    }
}
