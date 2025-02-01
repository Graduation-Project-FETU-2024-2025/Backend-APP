using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
