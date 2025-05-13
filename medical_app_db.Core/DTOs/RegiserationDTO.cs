using Microsoft.AspNetCore.Http;
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
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$",
            ErrorMessage = "Passord should be At least 6 characters,At least 1 uppercase,At least 1 lowercase,At least 1 digit,At least 1 Specila charchter like (_)")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password Must Match Pasword")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$",
            ErrorMessage = "Passord should be At least 6 characters,At least 1 uppercase,At least 1 lowercase,At least 1 digit,At least 1 Specila charchter like (_)")]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [AllowedValues("Male" , "Female","M","F","female","male",
            ErrorMessage = "Gender Must be Male,Female,M,F,female or male")]
        public string Gnder { get; set; } = null!;
        [Required]
        public string SSN { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        public IFormFile? Image { get; set; }

        [AllowNull]
        public Guid ClinicId { get; set; }
        [AllowNull]
        public Guid PharmacyId { get; set; }
        [AllowNull]
        public Guid SpecializationId { get; set; }
    }
}
