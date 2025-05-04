using System.Threading.Tasks;
using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Interfaces
{
    public interface IDoctorProfileServices
    {
        Task<DoctorDTO> GetDoctorProfileAsync();
        Task<DoctorDTO> UpdateDoctorProfileAsync(DoctorDTO doctorDto);
    }
}
