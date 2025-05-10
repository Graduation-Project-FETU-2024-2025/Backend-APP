using medical_app_db.Core.Models.Doctor_Module;

namespace medical_app_db.Core.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public float Rate { get; set; }
        public string? Comment { get; set; }
    }
}
