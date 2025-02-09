using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Migrations;
using medical_app_db.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace medical_app_api.Controllers
{
	[Route("api/[controller]")]
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

		[Authorize]
		[HttpDelete("{branch_id}/{product_id}")]
		public async Task<IActionResult> DeleteBranchProduct(Guid branch_id, Guid product_id)
		{
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
