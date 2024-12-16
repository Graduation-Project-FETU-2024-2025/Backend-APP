using System.Net;
using System.Text.Json.Serialization;
namespace medical_app_db.Core.Models
{
    public class AuthModel
    {
        public bool IsAuthuntecated { get; set; }
        public string? Message { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public ICollection<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
