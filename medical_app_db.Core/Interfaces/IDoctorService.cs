public interface IDoctorService
{
    Task<PaginatedResult<DoctorListDto>> GetAllDoctorsAsync(int pageNumber, int pageSize);
    Task<DoctorListDto?> GetDoctorByIdAsync(Guid id);
    Task<PaginatedResult<DoctorListDto>> GetDoctorsBySpecializationAsync(Guid specializationId, int pageNumber, int pageSize);
    Task<PaginatedResult<DoctorListDto>> GetTopRatedDoctorsAsync(int pageNumber, int pageSize);
    Task<PaginatedResult<DoctorListDto>> GetTopRatedDoctorsBySpecializationAsync(Guid specializationId, int pageNumber, int pageSize);
}
