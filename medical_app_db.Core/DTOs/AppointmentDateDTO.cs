using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
	public class AppointmentDateDTO
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public int AppointmentMaxNumber { get; set; }

		public ICollection<WorkingPeriodInClinic>? WorkingPeriods { get; set; }
	}
}
