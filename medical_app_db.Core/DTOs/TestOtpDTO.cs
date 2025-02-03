using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class TestOtpDTO
    {
        [Required]
        [Length(4,4)]
        public string? OTP { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
