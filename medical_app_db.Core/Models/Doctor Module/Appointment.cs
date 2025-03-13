namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Appointment
    {
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; } = null!;
		public Guid ClinicId { get; set; }

		public Clinic? Clinic { get; set; }
		public Guid UserId { get; set; }
		public User? User { get; set; }
		public String UserName { get; set; } = null!;
		public String DoctorName { get; set; } = null!;
		public float price { get; set; }

	}
}