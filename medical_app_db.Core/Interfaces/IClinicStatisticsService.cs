using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Interfaces
{
    public interface IClinicStatisticsService
    {
        Task<ClinicStatisticsDTO> GetClinicStatisticsAsync(Guid clinicId);
    }
}