using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Models
{
	public class BranchProduct
	{
		public Guid BranchId { get; set; }
		public Branch Branch { get; set; } = null!;

		public Guid SystemProductId { get; set; }
		public SystemProduct SystemProduct { get; set; } = null!;
	}
}
