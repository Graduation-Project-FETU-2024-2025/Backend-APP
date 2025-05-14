public class ReviewDto
{
    public Guid Id { get; set; } 
    public Guid ClinicId { get; set; }
    public float Rate { get; set; }
    public string? Comment { get; set; }
   
}
