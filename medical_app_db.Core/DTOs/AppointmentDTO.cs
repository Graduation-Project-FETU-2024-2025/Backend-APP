using medical_app_db.Core.Models.Doctor_Module;
using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Guid ClinicId { get; set; }
        [Display(Name = "Clinic Name")]
        public string? ClinicName { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? UserImage { get; set; }
        public string Type { get; set; }
    }
}
