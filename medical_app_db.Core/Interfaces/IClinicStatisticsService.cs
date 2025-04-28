using medical_app_db.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Interfaces
{
    public interface IClinicStatisticsService
    {
        Task<ClinicStatisticsDTO> GetClinicStatisticsAsync(Guid clinicId);
    }

}

