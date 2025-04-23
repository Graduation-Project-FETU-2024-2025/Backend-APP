namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public AppointmentType Type { get; set; }
        public Guid ClinicId { get; set; }
        public Clinic? Clinic { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string UserName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
