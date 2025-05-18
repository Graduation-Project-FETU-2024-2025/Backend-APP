
namespace medical_app_db.Core.DTOs
{
        public class DoctorDTO
        {
            public Guid Id { get; set; }
            public Guid ApplicationUserId { get; set; }
            public string? EN_Name { get; set; }
           public string? AR_Name { get; set; }
          public DateOnly DateOfBirth { get; set; }
            public string? Picture { get; set; }
            public string? Specialization { get; set; }
            public string? Gender { get; set; }
            public string? SSN { get; set; }
        }
 }
