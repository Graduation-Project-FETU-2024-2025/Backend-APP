using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Models
{
	public class SystemProduct
	{
		public String Code { get; set; } = null!;
		public String AR_Name { get; set; } = null!;
		public String? EN_Name { get; set; }
		public String? Image { get; set; }

		public ICollection<BranchProduct>? BranchProducts { get; set; }

	}
}
