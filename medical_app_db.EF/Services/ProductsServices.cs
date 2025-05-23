﻿using Azure.Core;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using medical_app_db.EF.Migrations;
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
		var httpContext = _httpContextAccessor.HttpContext ?? throw new UnauthorizedAccessException("HttpContext is not available.");
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

	public async Task<IEnumerable<ProductDTO>> GetAllBranchProductsAsync(Guid branchID, int page = 1, int pageSize = 3, String search = "")
	{
		var httpContext = _httpContextAccessor.HttpContext ?? throw new UnauthorizedAccessException("HttpContext is not available.");
        var BranchProducts = await _context.BranchProducts
			.Where(b => b.BranchId == branchID)
			.Where(b => b.SystemProduct.AR_Name.Contains(search) || b.SystemProduct.EN_Name.Contains(search))
			.Select(b => new ProductDTO
			{
				BranchId = branchID,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				productDTO = new SystemProductDTO
				{
					Code = b.SystemProduct.Code,
					AR_Name = b.SystemProduct.AR_Name,
					EN_Name = b.SystemProduct.EN_Name,
					Image = b.SystemProduct.Image,
					Type = b.SystemProduct.Type,
					Active_principal = b.SystemProduct.Active_principal,
					Company_Name = b.SystemProduct.Company_Name
				}
			})
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return BranchProducts;
	}

	public async Task<IEnumerable<ProductDTO>> GetOutOfStockProductsAsync(int page, int pageSize, string lang)
    {
        var branches = await GetAccountBranchs();
		var result = new Dictionary<Guid, string?>();
        foreach (var item in branches)
        {
			result.Add(item.BranchId, lang == "en" ? item.Branch.EN_BranchName : item.Branch.AR_BranchName);
        }
        var outOfStckProducts = await _context.BranchProducts
			.Where(p => result.Keys.Contains(p.BranchId))
			.Where(p => p.stock <= 5)
			.OrderBy(p => p.SystemProductCode)
            .Select(b => new ProductDTO
            {
                BranchId = b.BranchId,
                BranchName = result[b.BranchId],
                SystemProductCode = b.SystemProductCode,
                stock = b.stock,
                price = b.price,
                visibility = b.visibility,
                productDTO = new SystemProductDTO
                {
                    Code = b.SystemProduct.Code,
                    AR_Name = b.SystemProduct.AR_Name,
                    EN_Name = b.SystemProduct.EN_Name,
                    Image = b.SystemProduct.Image,
                    Type = b.SystemProduct.Type,
                    Active_principal = b.SystemProduct.Active_principal,
                    Company_Name = b.SystemProduct.Company_Name
                }
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

		return outOfStckProducts;
    }
    public async Task<IEnumerable<ProductDTO>> GetOutOfStockProductsAsyncByBranch(Guid branchId, int page, int pageSize, string lang)
	{
        var outOfStckProducts = await _context.BranchProducts
            .Where(p => p.BranchId == branchId)
            .Where(p => p.stock <= 5)
			.Include(p => p.Branch)
            .OrderBy(p => p.SystemProductCode)
            .Select(b => new ProductDTO
            {
                BranchId = b.BranchId,
                BranchName = lang  == "en" ? b.Branch.EN_BranchName : b.Branch.AR_BranchName,
                SystemProductCode = b.SystemProductCode,
                stock = b.stock,
                price = b.price,
                visibility = b.visibility,
                productDTO = new SystemProductDTO
                {
                    Code = b.SystemProduct.Code,
                    AR_Name = b.SystemProduct.AR_Name,
                    EN_Name = b.SystemProduct.EN_Name,
                    Image = b.SystemProduct.Image,
                    Type = b.SystemProduct.Type,
                    Active_principal = b.SystemProduct.Active_principal,
                    Company_Name = b.SystemProduct.Company_Name
                }
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return outOfStckProducts;
    }

    private async Task<IReadOnlyList<AccountBranch>> GetAccountBranchs()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        _ = Guid.TryParse(httpContext.User.FindFirst("Account")?.Value, out Guid accountId);

        var branches = await _context.AccountBranches
            .Where(ab => ab.AccountId == accountId)
			.Include(ab => ab.Branch)
            .ToListAsync();

        return branches;
    }
    public async Task<IEnumerable<ProductDTO>> GetLastAddedProductsByBranchAsync(Guid branchID)
	{
		var httpContext = _httpContextAccessor.HttpContext ?? throw new UnauthorizedAccessException("HttpContext is not available.");
		var lastAddedProducts = await _context.BranchProducts
			.Where(p => p.BranchId == branchID)
			.OrderByDescending(p => p.AdditionDate)
			.Select(b => new ProductDTO
			{
				BranchId = b.BranchId,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				AdditionDate = b.AdditionDate,
				productDTO = new SystemProductDTO
				{
					Code = b.SystemProduct.Code,
					AR_Name = b.SystemProduct.AR_Name,
					EN_Name = b.SystemProduct.EN_Name,
					Image = b.SystemProduct.Image,
					Type = b.SystemProduct.Type,
					Active_principal = b.SystemProduct.Active_principal,
					Company_Name = b.SystemProduct.Company_Name
				}
			})
			.Take(5)
			.ToListAsync();

		return lastAddedProducts;
	}

	public async Task<IEnumerable<ProductDTO>> GetLastAddedProductsAsync()
	{
		var accountBranches = await GetAccountBranchs();
		var branchIds = accountBranches.Select(b => b.BranchId).ToList();
		var httpContext = _httpContextAccessor.HttpContext ?? throw new UnauthorizedAccessException("HttpContext is not available.");
		var lastAddedProducts = await _context.BranchProducts
			.Where(p => branchIds.Contains(p.BranchId))
			.OrderByDescending(p => p.AdditionDate)
			.Select(b => new ProductDTO
			{
				BranchId = b.BranchId,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				AdditionDate = b.AdditionDate,
				productDTO = new SystemProductDTO
				{
					Code = b.SystemProduct.Code,
					AR_Name = b.SystemProduct.AR_Name,
					EN_Name = b.SystemProduct.EN_Name,
					Image = b.SystemProduct.Image,
					Type = b.SystemProduct.Type,
					Active_principal = b.SystemProduct.Active_principal,
					Company_Name = b.SystemProduct.Company_Name
				}
			})
			.Take(5)
			.ToListAsync();

		return lastAddedProducts;
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

		Boolean isValidBranchId = _context.Branches.Where(p => p.Id == productDto.BranchId).Count() > 0;
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
			var exsistingProduct = await _context
				.BranchProducts
				.FirstOrDefaultAsync(bp => 
					bp.BranchId == productDto.BranchId && bp.SystemProductCode == productDto.SystemProductCode);

            if (exsistingProduct is not null)
				throw new Exception("Product already exisit in the Branch") ;
			
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
			throw new Exception("Error occurred while adding Product: " + ex.Message);
		}
	}
	public async Task<ProductDTO> UpdateBranchProductAsync(Guid branch_id, Guid product_code, ProductDTO productDto)
	{
		// validate that branch is available to the account
		try
		{
			var branchProduct = await _context.BranchProducts
				.FirstOrDefaultAsync(b => b.BranchId == branch_id && b.SystemProductCode == product_code);

			if (branchProduct == null)
				return null;

			ValidateBranchProductData(productDto);

			branchProduct.price = productDto.price;
			branchProduct.visibility = productDto.visibility;
			branchProduct.stock = productDto.stock;

			await _context.SaveChangesAsync();
			return productDto;
		}
		catch (ArgumentException argEx)
		{
			throw new Exception("Validation error: " + argEx.Message);
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while updating branch product: " + ex.Message);
		}
	}
	public async Task<bool> DeleteBranchProductAsync(Guid branch_id, Guid product_code)
	{
		// validate that branch is available to the account
		var product = await _context.BranchProducts
			.FirstOrDefaultAsync(p => p.BranchId == branch_id && p.SystemProductCode == product_code);

		if (product == null)
			return false;

		_context.BranchProducts.Remove(product);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<ProductDTO> GetBranchProductAsync(Guid branchID, Guid productCode)
	{
		var branchProduct = await _context.BranchProducts.FirstOrDefaultAsync(b => b.BranchId == branchID && b.SystemProductCode == productCode);
		if (branchProduct == null)
			return null;

		var systemProduct = await _context.SystemProducts.Where(b => b.Code == productCode).FirstOrDefaultAsync();

		return new ProductDTO
		{
			BranchId = branchID,
			SystemProductCode = productCode,
			stock = branchProduct.stock,
			price = branchProduct.price,
			visibility = branchProduct.visibility,
			productDTO = new SystemProductDTO
			{
				Code = productCode,
				AR_Name = systemProduct!.AR_Name,
				EN_Name = systemProduct.EN_Name,
				Image = systemProduct.Image,
				Type = systemProduct.Type,
				Active_principal = systemProduct.Active_principal,
				Company_Name = systemProduct.Company_Name,
			}
		};
	}
}

