using medical_app_db.Core.DTOs;

public class DoctorListDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } 
    public string? ClinicName { get; set; }
    public string? ClinicAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public double? Rating { get; set; }
    public int ReviewsCount { get; set; }
    public string? Image { get; set; }
    public string? NextAvailableAppointment { get; set; }
    public decimal Price { get; set; }
    public string? About { get; set; }
    public SpecializationDto? Specialization { get; set; }
}
