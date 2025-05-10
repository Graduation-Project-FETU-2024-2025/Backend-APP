namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public required string ArName { get; set; }
        public required string EnName { get; set; }
        public string? Icon { get; set; }
    }
}
