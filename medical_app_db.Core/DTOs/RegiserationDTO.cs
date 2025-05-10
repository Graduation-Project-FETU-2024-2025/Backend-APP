using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace medical_app_db.Core.DTOs
{
    public class RegisterationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100), MinLength(3)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(100), MinLength(3)]
        public string FullName { get; set; } = null!;

        public string Role { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Gnder { get; set; } = null!;
        [Required]
        public string SSN { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public string Image { get; set; }

        [AllowNull]
        public Guid ClinicId { get; set; }
        [AllowNull]
        public Guid PharmacyId { get; set; }
        [AllowNull]
        public string? Specialization { get; set; }
        [JsonIgnore]
        public Guid SpecializationId { get; set; }
    }
}
