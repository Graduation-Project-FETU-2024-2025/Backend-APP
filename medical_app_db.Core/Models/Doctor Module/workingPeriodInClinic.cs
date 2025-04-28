using System;
namespace medical_app_db.Core.Models
{
    public class WorkingPeriodInClinic
    {
        public Guid AppointmentDateId { get; set; }
        public AppointmentDates? AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}