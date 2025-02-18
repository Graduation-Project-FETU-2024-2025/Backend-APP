using medical_app_db.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
	public class SystemProductDTO
	{
		public Guid Code { get; set; }
		public string AR_Name { get; set; } = null!;
		public string? EN_Name { get; set; }
		public string? Image { get; set; }
		public string? Type { get; set; }
		public string? Active_principal { get; set; }
		public string? Company_Name { get; set; }
	}
}
