using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class PrescriptionProductDTO
    {
        [Required]
        public Guid SystemProductCode { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
