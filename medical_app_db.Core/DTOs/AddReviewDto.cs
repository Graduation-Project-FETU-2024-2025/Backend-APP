public class AddReviewDto
{
    public Guid ClinicId { get; set; }
    public float Rate { get; set; }
    public string? Comment { get; set; }
}
