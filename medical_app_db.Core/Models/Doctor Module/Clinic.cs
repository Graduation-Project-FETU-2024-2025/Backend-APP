namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }

        public DoctorClinic? DoctorClinic { get; set; }
        public ICollection<ClinicPhone>? ClinicPhones { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<AppointmentDates>? AppointmentDates { get; set; }
    }
}
