using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPasswrod { get; set; }
        [Required]
        public string? Token { get; set; }

    }
}
