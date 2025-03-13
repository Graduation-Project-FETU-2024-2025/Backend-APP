using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Models.Doctor_Module
{
	public class DoctorClinic
	{
		public Guid DoctorId { get; set; }
		public Doctor Doctor { get; set; } = null!;
		public Guid ClinicId { get; set; }

		public Clinic Clinic { get; set; } = null!;
	}
}
