using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Interfaces
{
    public interface IProductService
    {
		Task<IEnumerable<SystemProductDTO>> GetAllSystemProductsAsync(int page, int pageSize, String search);
		Task<IEnumerable<ProductDTO>> GetAllBranchProductsAsync(Guid branchID, int page, int pageSize, String search);
		Task<IEnumerable<ProductDTO>>GetOutOfStockProductsAsyncByBranch(Guid branchId, int page, int pageSize, string lang);
        Task<IEnumerable<ProductDTO>> GetOutOfStockProductsAsync(int page, int pageSize, string lang);
		Task<IEnumerable<ProductDTO>> GetLastAddedProductsByBranchAsync(Guid branchID);
		Task<IEnumerable<ProductDTO>> GetLastAddedProductsAsync();
		Task<ProductDTO> GetBranchProductAsync(Guid branchID, Guid productCode);
		Task<ProductDTO> AddBranchProductAsync(ProductDTO productDto);
		Task<ProductDTO> UpdateBranchProductAsync(Guid branchID, Guid productCode, ProductDTO productDto);
		Task<bool> DeleteBranchProductAsync(Guid branch_id, Guid product_id);
    }
}
