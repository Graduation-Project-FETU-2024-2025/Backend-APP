using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.EF.Services;

	public class ClinicService
	{
		private readonly MedicalDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClinicService(MedicalDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<ClinicDTO> GetClinicByIdAsync(Guid id)
		{
			var clinicId = (Guid)_httpContextAccessor.HttpContext.Items["ClinicId"];

			var clinic = await _context.Clinics.Include(b => b.AppointmentDates)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (clinic == null)
				throw new KeyNotFoundException("Clinic not found");

			return new ClinicDTO
			{
				Id = clinic.Id,
				Name = clinic.Name,
				Address = clinic.Address,
				Price = clinic.Price,
				Long = clinic.Long,
				Lat = clinic.Lat,
				DoctorClinic = clinic.DoctorClinic,
				ClinicPhones = clinic.ClinicPhones,
				Appointments = clinic.Appointments,
				AppointmentDates = clinic.AppointmentDates,
			};
		}
	}
}
