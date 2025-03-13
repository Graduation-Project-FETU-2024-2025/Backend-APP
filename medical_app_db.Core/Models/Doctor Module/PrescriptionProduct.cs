using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Models.Doctor_Module
{
	public class PrescriptionProduct
	{
		public Guid PrescriptionId { get; set; }

		public Prescription Prescription { get; set; } = null!;
		public Guid SystemProductCode { get; set; }
		public SystemProduct SystemProduct { get; set; } = null!;
		public String Description { get; set; } = null!;

	}
}
