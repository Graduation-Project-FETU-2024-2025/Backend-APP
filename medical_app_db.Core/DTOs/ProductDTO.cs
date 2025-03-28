using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.DTOs
{
	public class ProductDTO
	{
		public Guid BranchId { get; set; }
		public Guid SystemProductCode { get; set; }
		public int stock { get; set; }
		public float price { get; set; }
		public bool visibility { get; set; } = false;
        public DateOnly AdditionDate { get; set; }
        public SystemProductDTO? productDTO { get; set; } = null;
	}
}
