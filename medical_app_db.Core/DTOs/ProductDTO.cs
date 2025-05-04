using System.ComponentModel.DataAnnotations;

namespace medical_app_db.Core.DTOs
{
	public class ProductDTO
	{
		[Required]
		public Guid BranchId { get; set; }
		[Required]
		public Guid SystemProductCode { get; set; }
		[Required]
		public int stock { get; set; }
		[Required]
		public float price { get; set; }
		[Required]
		public bool visibility { get; set; } = false;
        public string? BranchName { get; set; }
        public DateOnly AdditionDate { get; set; }
        public SystemProductDTO? productDTO { get; set; } = null;
	}
}
