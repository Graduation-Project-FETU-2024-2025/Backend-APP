using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class PrescriptionDTO
    {
        [Required]
        public Guid DoctorId { get; set; }
        [Required]
        public Guid AppointmentId { get; set; }
        public string? Tests { get; set; }
        public string? Notes { get; set; }
        public ICollection<PrescriptionProductDTO>? PrescriptionProductDTOs { get; set; }
    }
}
