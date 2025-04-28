using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Migrations;
using medical_app_db.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
	[Authorize]
	[Route("api/secure/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> getSystemProducts(int page = 1, int pageSize = 3, String search = "")
		{
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			var systemProducts = await _productService.GetAllSystemProductsAsync(page, pageSize, search);

			if (systemProducts == null || !systemProducts.Any())
			{
				return NoContent();
			}

			var result = systemProducts.Select(b => new
			{
				Code = b.Code,
				Name = lang == "ar" ? b.AR_Name : b.EN_Name,
				Image = b.Image,
				Type = b.Type,
				Active_principal = b.Active_principal,
				Company_Name = b.Company_Name
			});

			return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("{branch_id}")]
		public async Task<IActionResult> getBranchProducts(Guid branch_id, int page = 1, int pageSize = 3, string search = "")
		{
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			var branchProducts = await _productService.GetAllBranchProductsAsync(branch_id, page, pageSize, search);

			if (branchProducts == null || !branchProducts.Any())
			{
				return NoContent();
			}

			var result = branchProducts.Select(b => new
			{
				BranchId = branch_id,
				Name = lang == "ar" ? b.productDTO.AR_Name : b.productDTO.EN_Name,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				productDTO = new SystemProductDTO
				{
					Code = b.productDTO.Code,
					AR_Name = b.productDTO.AR_Name,
					EN_Name = b.productDTO.EN_Name,
					Image = b.productDTO.Image,
					Type = b.productDTO.Type,
					Active_principal = b.productDTO.Active_principal,
					Company_Name = b.productDTO.Company_Name
				}
			});

			return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("out-of-stock")]
		public async Task<IActionResult> GetOutOfStockProducts(int page = 1, int pageSize = 3)
		{
            var lang = Request.Headers["lang"].ToString().ToLower();

            if (string.IsNullOrEmpty(lang))
            {
                return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
            }

			if (page < 1) page = 1;
			var outOfStckProducts = await _productService.GetOutOfStockProductsAsync(page, pageSize,lang);

            if (outOfStckProducts == null || !outOfStckProducts.Any())
            {
                return NoContent();
            }

            var result = outOfStckProducts.Select(b => new
            {
                b.BranchId,
				b.BranchName,
                Name = lang == "ar" ? b.productDTO.AR_Name : b.productDTO.EN_Name,
                b.SystemProductCode,
                b.stock,
                b.price,
                b.visibility,
                productDTO = new SystemProductDTO
                {
                    Code = b.productDTO.Code,
                    AR_Name = b.productDTO.AR_Name,
                    EN_Name = b.productDTO.EN_Name,
                    Image = b.productDTO.Image,
                    Type = b.productDTO.Type,
                    Active_principal = b.productDTO.Active_principal,
                    Company_Name = b.productDTO.Company_Name
                }
            });

            return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("last-added/{branch_id")]
		public async Task<IActionResult> GetLastAddedProductsByBranch(Guid branch_id)
		{
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			var lastAddedProducts = await _productService.GetLastAddedProductsByBranchAsync(branch_id);

			if (lastAddedProducts == null || !lastAddedProducts.Any())
			{
				return NoContent();
			}

			var result = lastAddedProducts.Select(b => new
			{
				BranchId = b.BranchId,
				Name = lang == "ar" ? b.productDTO.AR_Name : b.productDTO.EN_Name,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				productDTO = new SystemProductDTO
				{
					Code = b.productDTO.Code,
					AR_Name = b.productDTO.AR_Name,
					EN_Name = b.productDTO.EN_Name,
					Image = b.productDTO.Image,
					Type = b.productDTO.Type,
					Active_principal = b.productDTO.Active_principal,
					Company_Name = b.productDTO.Company_Name
				}
			});

			return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("last-added")]
		public async Task<IActionResult> GetLastAddedProducts()
		{
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			var lastAddedProducts = await _productService.GetLastAddedProductsAsync();

			if (lastAddedProducts == null || !lastAddedProducts.Any())
			{
				return NoContent();
			}

			var result = lastAddedProducts.Select(b => new
			{
				BranchId = b.BranchId,
				Name = lang == "ar" ? b.productDTO.AR_Name : b.productDTO.EN_Name,
				SystemProductCode = b.SystemProductCode,
				stock = b.stock,
				price = b.price,
				visibility = b.visibility,
				productDTO = new SystemProductDTO
				{
					Code = b.productDTO.Code,
					AR_Name = b.productDTO.AR_Name,
					EN_Name = b.productDTO.EN_Name,
					Image = b.productDTO.Image,
					Type = b.productDTO.Type,
					Active_principal = b.productDTO.Active_principal,
					Company_Name = b.productDTO.Company_Name
				}
			});

			return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
		}

		[HttpGet("{branch_id}/{product_code}")]
		public async Task<IActionResult> GetBranchProductById(Guid branch_id, Guid product_code)
		{
			// Validate the branch is accessibly to the account
			var lang = Request.Headers["lang"].ToString().ToLower();

			if (string.IsNullOrEmpty(lang))
			{
				return BadRequest(new { message = "Language not provided in the header.", statusCode = (int)HttpStatusCode.BadRequest });
			}

			try
			{
				var product = await _productService.GetBranchProductAsync(branch_id, product_code);

				if (product == null)
				{
					return NotFound(new { message = "Product not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				var result = new
				{
					BranchId = branch_id,
					Name = lang == "ar" ? product.productDTO.AR_Name : product.productDTO.EN_Name,
					SystemProductCode = product_code,
					stock = product.stock,
					price = product.price,
					visibility = product.visibility,
					productDTO = new
					{
						Code = product_code,
						AR_Name = product.productDTO.AR_Name,
						EN_Name = product.productDTO.EN_Name,
						Image = product.productDTO.Image,
						Type = product.productDTO.Type,
						Active_principal = product.productDTO.Active_principal,
						Company_Name = product.productDTO.Company_Name,
					}
				};

				return Ok(new { message = "Success", statusCode = (int)HttpStatusCode.OK, data = result });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddBranchProduct(ProductDTO productDto)
		{
			if (productDto == null)
			{
				return BadRequest(new { message = "Invalid product data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			// Validate that branch product isn't already added
			// Validate that branch id available for the account

			try
			{
				var createBranchProduct = await _productService.AddBranchProductAsync(productDto);

				return Ok(					
					new {
						message = "Branch Product has been added successfully",
						statusCode = (int)HttpStatusCode.Created,
						data = productDto
					});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to add branch product", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpPut("{branch_id}/{product_code}")]
		public async Task<IActionResult> UpdateBranchProduct(Guid branch_id, Guid product_code, ProductDTO productDTO)
		{
			if (productDTO == null)
			{
				return BadRequest(new { message = "Invalid branch product data", statusCode = (int)HttpStatusCode.BadRequest });
			}

			// Validate that branch id available to the account

			try
			{
				var updatedBranchProduct = await _productService.UpdateBranchProductAsync(branch_id, product_code, productDTO);

				if (updatedBranchProduct == null)
				{
					return NotFound(new { message = "Branch not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new
				{
					message = "Branch Product updated successfully",
					statusCode = (int)HttpStatusCode.OK,
					data = updatedBranchProduct
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to update branch product", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}

		[HttpDelete("{branch_id}/{product_id}")]
		public async Task<IActionResult> DeleteBranchProduct(Guid branch_id, Guid product_id)
		{
			// validate that branch available to the account
			try
			{
				var result = await _productService.DeleteBranchProductAsync(branch_id, product_id); // مرر الـ BranchId فقط

				if (!result)
				{
					return NotFound(new { message = "Product not found", statusCode = (int)HttpStatusCode.NotFound });
				}

				return Ok(new { message = "Product deleted successfully", statusCode = (int)HttpStatusCode.OK });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Failed to delete product", statusCode = (int)HttpStatusCode.InternalServerError, details = ex.Message });
			}
		}
	}
}
