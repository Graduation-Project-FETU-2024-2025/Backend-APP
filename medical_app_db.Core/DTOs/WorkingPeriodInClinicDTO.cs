using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
    public class WorkingPeriodInClinicDTO
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
