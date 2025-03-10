namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
