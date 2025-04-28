using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models.Doctor_Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Interfaces
{
	public interface IClinicService
	{
		public Task<ClinicDTO> GetClinicByIdAsync(Guid id);
		public Task<Clinic> UpdateClinicAsync(ClinicDTO clinicDTO);
	}
}
