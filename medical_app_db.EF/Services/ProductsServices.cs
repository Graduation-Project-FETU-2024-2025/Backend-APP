using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
namespace medical_app_db.Services;
public class ProductsServices : IProductService
{
    private readonly MedicalDbContext _context;

    public ProductsServices(MedicalDbContext context)
    {
        _context = context;
    }

	public async Task<bool> DeleteBranchProductAsync(Guid branch_id, Guid product_id)
	{
		var product = await _context.BranchProducts
			.FirstOrDefaultAsync(p => p.BranchId == branch_id && p.SystemProductId == product_id);

		if (product == null)
			return false;

		_context.BranchProducts.Remove(product);
		await _context.SaveChangesAsync();
		return true;
	}
}

