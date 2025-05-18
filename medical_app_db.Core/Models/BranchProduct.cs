namespace medical_app_db.Core.Models
{
	public class BranchProduct
	{
		public Guid BranchId { get; set; }
		public Branch Branch { get; set; } = null!;

		public Guid SystemProductCode { get; set; }
		public SystemProduct SystemProduct { get; set; } = null!;
		public int stock {  get; set; }
		public float price { get; set; }
		public bool visibility { get; set; } = false;
		public DateOnly AdditionDate { get; set; }
    }
}
