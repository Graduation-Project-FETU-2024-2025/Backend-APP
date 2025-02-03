using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetAllBranchesAsync();
        Task<BranchDTO> GetBranchByIdAsync(Guid id, string lang); // إضافة اللغة كـ parameter
        Task<BranchDTO> AddBranchAsync(BranchDTO branchDto);
        Task<BranchDTO> UpdateBranchAsync(Guid id, BranchDTO branchDto);
        Task<bool> DeleteBranchAsync(Guid id);
    }
}
