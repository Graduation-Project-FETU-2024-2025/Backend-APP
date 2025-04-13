namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Doctor : ApplicationUser
    {
        public string? Specialization { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
        public DoctorClinic? DoctorClinic { get; set; }
    }
}
