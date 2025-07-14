using medical_app_db.Core.DTOs;
using medical_app_db.Core.DTOs.Order;
using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Order_Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace medical_app_db.EF.Services
{
    public class PaymobService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly int _integrationId;
        private readonly string _iframeId = "YOUR_IFRAME_ID";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly PaymobSetting _paymobSetting;
        private readonly string _hmacSecret;

        public PaymobService(
            HttpClient httpClient,
            IOptions<PaymobSetting> paymobOptions,
            UserManager<ApplicationUser> userManager,
            IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
            _httpClient = httpClient;
            _paymobSetting = paymobOptions.Value;


            _hmacSecret = _paymobSetting.HmacSecret;
            _apiKey = _paymobSetting.ApiKey;
            _integrationId = int.Parse(_paymobSetting.IntegrationId);
            _iframeId = _paymobSetting.IFrameId;
        }

        public async Task<string> GetPayemntUrlAsync(PaymentRequest paymentRequest)
        {
            var order = await _orderService.GetOrderByIdAsync(paymentRequest.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }
            var token = await GetAuthTokenAsync();

            var user = await _userManager.FindByEmailAsync(order.UserEmail ?? "");
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var orderId = await CreateOrderAsync(token,order,paymentRequest);


            var payment_token = await CreatePaymentKeyAsync(token, orderId, order, user,paymentRequest);
            
            return $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={payment_token}";
        }
        private async Task<string> GetAuthTokenAsync()
        {
            var authRes = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", new { api_key = _apiKey });
            var authData = await authRes.Content.ReadFromJsonAsync<PaymentAuthResponse>();
            if (authData == null || string.IsNullOrEmpty(authData.token))
            {
                throw new Exception("Failed to retrieve Paymob authentication token.");
            }
            return authData.token;
        }
        private async Task<int> CreateOrderAsync(string token, OrderToReturnDTO order, PaymentRequest request)
        {
            var orderPayload = new
            {
                auth_token = token,
                delivery_needed = false,
                amount_cents = (int)(request.Amount * 100),
                merchant_order_id = order.Id,
                currency = "EGP",
                items = order.OrderItems.Select(item => new
                {
                    name = item.SystemProductName,
                    amount_cents = (int)(item.SystemProductPrice * 100),
                    description = "No description",
                    quantity = item.Quantity
                }).ToList()
            };
            string jsonPayload = System.Text.Json.JsonSerializer.Serialize(orderPayload, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine("SENT TO PAYMOB:");
            Console.WriteLine(jsonPayload);

            // Send to Paymob
            var orderRes = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", orderPayload);
            string paymobResponse = await orderRes.Content.ReadAsStringAsync();
            var res = await orderRes.Content.ReadFromJsonAsync<PaymentOrderResponse>();
            Console.WriteLine("RESPONSE FROM PAYMOB:");
            Console.WriteLine(paymobResponse);
            return res?.id ?? throw new Exception("Failed to create Paymob order.");
        }
        private async Task<string> CreatePaymentKeyAsync(
            string token, 
            int orderId, 
            OrderToReturnDTO order,
            ApplicationUser user,
            PaymentRequest request)
        {
            var billingData = new
            {
                apartment = "NA",
                email = user.Email ?? order.UserEmail ?? "test@example.com",
                floor = "NA",
                first_name = user.UserName ?? order.UserName.Split(" ")[0] ?? "Test",
                street = "NA",
                building = "NA",
                phone_number = user.PhoneNumber ?? "+201234567890",
                shipping_method = "NA",
                postal_code = "111111",
                city = "Cairo",
                country = "EG",
                last_name = "User",
                state = "NA"
            };

            var keyPayload = new
            {
                auth_token = token,
                amount_cents = (int)(order.TotalPrice * 100),
                expiration = 3600,
                order_id = orderId,
                billing_data = billingData,
                currency = "EGP",
                integration_id = _integrationId
            };

            var res = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", keyPayload);
            var json = await res.Content.ReadAsStringAsync();
            var keyData = await res.Content.ReadFromJsonAsync<PaymentKeyResponse>();
            Console.WriteLine("Payment key response: " + json);

            if (!res.IsSuccessStatusCode)
                throw new Exception("Failed to generate payment key: " + json);

            return keyData?.token ?? throw new Exception("Failed to create Paymob payment key.");
        }
        public async Task<PaymentCallBackDto?> ProcessPaymentCallbackAsync(string payload, string hmacReceived)
        {

            // Parse the callback payload
            var jsonDocument = JsonDocument.Parse(payload);
            var root = jsonDocument.RootElement;

            if (!root.TryGetProperty("obj", out var objElement))
            {
                return null;
            }
            // Extract transaction and order details
            //bool success = objElement.TryGetProperty("success", out var successElement);
            bool isSuccess = false;
            if (objElement.TryGetProperty("success", out JsonElement successElement))
            {
                isSuccess = successElement.GetBoolean();
                Console.WriteLine($"Success: {isSuccess}");
            }
            else
            {
                Console.WriteLine("Could not find the 'success' property.");
            }

            // Check if necessary properties exist
            if (!objElement.TryGetProperty("order", out var orderElement))
            {
                return null;
            }
            Guid merchantOrderId = Guid.Empty;
            if (orderElement.TryGetProperty("merchant_order_id", out var merchantOrderIdElement) &&
                Guid.TryParse(merchantOrderIdElement.GetString(), out var parsedOrderId))
            {
                merchantOrderId = parsedOrderId;
            }

            if (merchantOrderId == Guid.Empty)
            {
                return null;
            }

            // Extract amount and transaction ID
            int amountCents = 0;
            if (objElement.TryGetProperty("amount_cents", out var amountElement))
            {
                amountCents = amountElement.GetInt32();
            }

            string transactionId = "unknown";
            if (objElement.TryGetProperty("id", out var idElement))
            {
                transactionId = idElement.ToString();
            }
            var OrderId = 0;
            if (objElement.TryGetProperty("payment_key_claims", out var claimsElement) &&
               claimsElement.TryGetProperty("extra", out var extraElement) &&
                extraElement.TryGetProperty("OrderId", out var OrderIdElement))
            {
                OrderId = OrderIdElement.GetInt32();
            }
            // Create callback data object
            var callbackData = new PaymentCallBackDto
            {
                Success = isSuccess,
                OrderId = OrderId,
                merchantOrderId = merchantOrderId,
                Amount = amountCents / 100.0m, // Convert cents to currency units
                TransactionId = transactionId,
                TransactionDate = DateTime.UtcNow,
                RawData = payload
            };


            if (string.IsNullOrEmpty(_hmacSecret))
            {
                return callbackData;
            }

            var hmacSecret = _hmacSecret; // from appsettings

            //Convert JSON to Dictionary<string, object>
            var dataDict = JsonSerializer.Deserialize<Dictionary<string, object>>(objElement.ToString());

            if (!await ValidateHmac(dataDict!, hmacReceived, hmacSecret))
                return null;
            return callbackData;
        }

        private async Task<bool> ValidateHmac(Dictionary<string, object> data, string receivedHmac, string secretKey)
        {
            var fields = new[]
            {
                "amount_cents", "created_at", "currency", "error_occured", "has_parent_transaction", "id",
                "integration_id", "is_3d_secure", "is_auth", "is_capture", "is_refunded", "is_standalone_payment",
                "is_voided", "order.id", "owner", "pending", "source_data.pan", "source_data.sub_type",
                "source_data.type", "success"
            };

            var values = new List<string>();
            foreach (var field in fields)
            {
                string[] path = field.Split('.');
                object? current = data;
                if (current is Dictionary<string, object> dict && dict.TryGetValue(path[0], out var next))
                {
                    if (path.Length > 1)
                    {

                        var help = JsonSerializer.Deserialize<Dictionary<string, object>>((JsonElement)next);
                        help.TryGetValue(path[1], out var next2);
                        if (next2.ToString() == "True" || next2.ToString() == "False")
                        {
                            next2 = next2.ToString().ToLower();
                        }
                        current = next2;
                    }
                    else
                    {
                        if (next.ToString() == "True" || next.ToString() == "False")
                        {
                            next = next.ToString().ToLower();
                        }
                        current = next;
                    }
                }
                else
                {
                    current = null;
                    break;
                }

                values.Add(current?.ToString() ?? string.Empty);
            }

            string concatenated = string.Concat(values);

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(concatenated));
            var computedHmac = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            return (computedHmac == receivedHmac.ToLowerInvariant());
        }

    }
}
