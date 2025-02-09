using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace medical_app_db.Services;
public class ProductsServices : IProductService
{
    private readonly MedicalDbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public ProductsServices(MedicalDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
		_httpContextAccessor = httpContextAccessor;

	}

	public async Task<IEnumerable<SystemProductDTO>> GetAllSystemProductsAsync(int page = 1, int pageSize = 3, String search = "")
	{
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext == null)
		{
			throw new UnauthorizedAccessException("HttpContext is not available.");
		}

		var SystemProducts = await _context.SystemProducts
			.Where(b => b.AR_Name.Contains(search) || b.EN_Name.Contains(search))
			.Select(b => new SystemProductDTO
			{
				Code = b.Code,
				AR_Name = b.AR_Name,
				EN_Name = b.EN_Name,
				Image = b.Image,
				Type = b.Type,
				Active_principal = b.Active_principal,
				Company_Name = b.Company_Name
			})
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return SystemProducts;
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

