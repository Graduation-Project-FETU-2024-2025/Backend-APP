using medical_app_db.Core.DTOs;
namespace medical_app_db.Core.Helpers
{
    public class ProfileServiceResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public UserHistoryDTO? Data { get; set; }
    }
}
