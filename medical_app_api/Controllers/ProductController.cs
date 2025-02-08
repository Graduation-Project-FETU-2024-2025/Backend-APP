using medical_app_db.Core.Interfaces;
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

		[HttpDelete("{branch_id}/{product_id}")]
		public async Task<IActionResult> DeleteBranchProduct(Guid branch_id, Guid product_id)
		{
			try
			{
				var result = await _productService.DeleteProductAsync(branch_id, product_id); // مرر الـ BranchId فقط

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
