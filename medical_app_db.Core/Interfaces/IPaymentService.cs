using medical_app_db.Core.DTOs;
using medical_app_db.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<string> GetPayemntUrlAsync(PaymentRequest paymentRequest);
        Task<PaymentCallBackDto> ProcessPaymentCallbackAsync(string payload, string hmacRecieved);
    }
}
