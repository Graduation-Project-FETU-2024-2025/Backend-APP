﻿using medical_app_db.Core.Models.Doctor_Module;
using System;

namespace medical_app_db.Core.Models
{
    public class AppointmentDates
    {
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; } 
        public Clinic? Clinic { get; set; }
        public DateTime Date { get; set; }
        public int AppointmentMaxNumber { get; set; } 

        public ICollection<WorkingPeriodInClinic> WorkingPeriods { get; set; } = null!;
    }
}


