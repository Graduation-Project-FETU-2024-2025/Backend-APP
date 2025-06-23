using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
    public class UserHistoryDTO
    {
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public AppointmentDTO? Appointment { get; set; }
    }
}
