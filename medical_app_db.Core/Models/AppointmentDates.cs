using System;

namespace medical_app_db.Core.Models
{
    public class AppointmentDates
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; } 
        public Clinic Clinic { get; set; } = null!;
        public DateTime Date { get; set; }
        public int AppointmentMaxNumber { get; set; } 

        public ICollection<WorkingPeriodInClinic>? WorkingPeriods { get; set; }
    }
}


