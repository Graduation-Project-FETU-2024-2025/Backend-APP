using System.Net;

namespace medical_app_db.Core.Helpers
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
