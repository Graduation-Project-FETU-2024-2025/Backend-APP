using System.Net;

namespace medical_app_db.Core.DTOs.Order
{
    public class OrderServiceResult
    {
        public bool Succeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public OrderToReturnDTO? Data { get; set; }
    }
}
