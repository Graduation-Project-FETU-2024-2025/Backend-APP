using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class RegisterationDTO
    {
        [Required]
        public Guid PharmacyId { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100), MinLength(3)]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public string? Image { get; set; }
    }
}
