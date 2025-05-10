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
using System.Security.Claims;
using medical_app_db.Core.Interfaces;
using AutoMapper;

namespace medical_app_db.EF.Services;

public class ClinicService : IClinicService
{
	private readonly MedicalDbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public ClinicService(MedicalDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		_context = context;
		_httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
	private Guid GetClinicId()
	{
		bool success = Guid.TryParse(_httpContextAccessor.HttpContext.User
		   .FindFirstValue("ClinicId"), out Guid ClinicId);

		if (!success)
			throw new UnauthorizedAccessException("Unothorized to Access This Resourse");

		return ClinicId;
	}

	public async Task<ClinicDTO> GetClinicByIdAsync(Guid id)
	{
		Guid.TryParse(_httpContextAccessor.HttpContext.User
            .FindFirstValue("ClinicId"), out Guid ClinicId);

		var clinic = await _context.Clinics
			.Include(c => c.AppointmentDates)
			.Include(c => c.ClinicPhones)
			.Include(c => c.DoctorClinic)
			.FirstOrDefaultAsync(b => b.Id == id);

		if (clinic == null)
			throw new KeyNotFoundException("Clinic not found");

		clinic.AppointmentDates = await _context.AppointmentDates
			.Where(ad => ad.ClinicId == clinic.Id)
			.Include(ad => ad.WorkingPeriods)
			.ToListAsync();

        var doctor = await _context.Set<Doctor>()
			.Include(d => d.Specialization)
            .FirstOrDefaultAsync(d => d.Id == clinic.DoctorClinic.DoctorId);

        return new ClinicDTO
		{
			Id = clinic.Id,
			Name = clinic.Name,
			Address = clinic.Address,
			Price = clinic.Price,
			Long = clinic.Long,
			Lat = clinic.Lat,
			Specialization = doctor?.Specialization?.Name ?? "",
			AppointmentDates = _mapper.Map<List<AppointmentDateDTO>>(clinic.AppointmentDates),
			ClinicPhones = _mapper.Map<List<ClinicPhonesDTO>>(clinic.ClinicPhones)
		};
	}
	public async Task<ClinicDTO> UpdateClinicAsync(ClinicDTO clinicDTO)
	{
		var clinicID = GetClinicId();
		var clinic = await _context.Clinics
			.Include(c => c.DoctorClinic)
			.FirstOrDefaultAsync(c => c.Id == clinicID);

		if (clinic == null)
			return null;

		var doctor = await _context.Set<Doctor>().Include(d => d.Specialization)
			.FirstOrDefaultAsync(d => d.Id == clinic.DoctorClinic.DoctorId);

		clinic.Name = clinicDTO.Name;
		clinic.Price = clinicDTO.Price;
		clinic.Lat = clinicDTO.Lat;
		clinic.Long = clinicDTO.Long;
		doctor.Specialization.Name = clinicDTO.Specialization;


		await _context.SaveChangesAsync();
		return await GetClinicByIdAsync(clinicID);
	}

}

