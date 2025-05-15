using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
	public class SpecializationDto
	{
		public Guid Id { get; set; }
		public required string ArName { get; set; }
		public required string EnName { get; set; }
		public string? Icon { get; set; }
	}
}
