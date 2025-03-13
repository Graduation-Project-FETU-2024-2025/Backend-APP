namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid ApplicationUserId { get; set; }
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Picture { get; set; }
        public string? Specialization { get; set; }
        public string? Gnder { get; set; }
        public string? SSN { get; set; }
    }
}
