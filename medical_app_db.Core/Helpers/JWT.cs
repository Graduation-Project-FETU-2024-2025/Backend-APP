namespace medical_app_db.Core.Helpers
{
    public class JWT
    {
        public string? SecurityKey { get; set; }
        public string? AudienceIP { get; set; }
        public string? IssureIP { get; set; }
        public double DurationInDays { get; set; }
    }
}
