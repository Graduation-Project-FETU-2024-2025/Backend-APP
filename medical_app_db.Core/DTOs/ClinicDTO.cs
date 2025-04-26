using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
	public class ClinicDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Address { get; set; }
		public decimal Price { get; set; }
		public double Long { get; set; }
		public double Lat { get; set; }

		public ICollection<ClinicPhonesDTO>? ClinicPhones { get; set; }
		public ICollection<AppointmentDateDTO>? AppointmentDates { get; set; }
	}
}
