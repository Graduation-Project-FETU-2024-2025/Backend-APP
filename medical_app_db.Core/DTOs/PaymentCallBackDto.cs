using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
    public class PaymentCallBackDto
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public Guid merchantOrderId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RawData { get; set; }
    }
}
