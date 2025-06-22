using medical_app_db.Core.Models.Doctor_Module;
namespace medical_app_db.Core.Models
{
	public class SystemProduct
	{
		public Guid Code { get; set; }
		public string AR_Name { get; set; } = null!;
		public string? EN_Name { get; set; }
		public string? Image { get; set; }
		public string ?Type {  get; set; }
		public string ?Active_principal {  get; set; }
		public string ?Company_Name {  get; set; }
        public ICollection<BranchProduct>? BranchProducts { get; set; }
        public ICollection<PrescriptionProduct>? PrescriptionProducts { get; set; }

    }
}
