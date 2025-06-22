namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Doctor : ApplicationUser
    {
        public Guid SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Prescription>? Prescriptions { get; set; }
        public DoctorClinic? DoctorClinic { get; set; }
        public string FirstName { get; set; }
    }
}
