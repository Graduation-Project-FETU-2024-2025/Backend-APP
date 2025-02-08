using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> DeleteBranchProductAsync(Guid branch_id, Guid product_id);
    }
}
