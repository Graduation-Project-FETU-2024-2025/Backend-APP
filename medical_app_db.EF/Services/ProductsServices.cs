using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
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

	private void ValidateBranchProductData(ProductDTO productDto)
	{
		if(productDto.stock < 0)
		{
			throw new InvalidDataException("Stock must be greater or equal than 0");
		}

		if (productDto.price < 0)
		{
			throw new InvalidDataException("Price must be greate or equal than 0");
		}

		Boolean isValidBranchId = _context.BranchProducts.Where(p => p.BranchId == productDto.BranchId).Count() > 0;
		if (!isValidBranchId)
		{
			throw new InvalidDataException("Branch isn't exists");
		}
		Boolean isValidProductId = _context.SystemProducts.Where(p => p.Code == productDto.SystemProductCode).Count() > 0;
		if (!isValidProductId)
		{
			throw new InvalidDataException("Product isn't exists");
		}
	}

	public async Task<ProductDTO> AddBranchProductAsync(ProductDTO productDto)
	{
		try
		{
			ValidateBranchProductData(productDto);

			var product = new BranchProduct
			{
				BranchId = productDto.BranchId,
				SystemProductCode = productDto.SystemProductCode,
				stock = productDto.stock,
				price = productDto.price,
				visibility = productDto.visibility,
			};

			_context.BranchProducts.Add(product);
			await _context.SaveChangesAsync();

			return productDto;
		}
		catch (ArgumentException argEx)
		{
			throw new Exception("Validation error: " + argEx.Message);
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while adding branch: " + ex.Message);
		}
	}
	public async Task<bool> DeleteBranchProductAsync(Guid branch_id, Guid product_id)
	{
		var product = await _context.BranchProducts
			.FirstOrDefaultAsync(p => p.BranchId == branch_id && p.SystemProductCode == product_id);

		if (product == null)
			return false;

		_context.BranchProducts.Remove(product);
		await _context.SaveChangesAsync();
		return true;
	}
}

