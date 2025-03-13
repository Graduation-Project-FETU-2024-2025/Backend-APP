using medical_app_db.Core.Models.Doctor_Module;
using System;

namespace medical_app_db.Core.Models
{
    public class ClinicPhone
    {
        public int C_ID { get; set; } 
        public string PhoneNumber { get; set; } = null!; 

        public Clinic Clinic { get; set; } = null!;
    }

}
