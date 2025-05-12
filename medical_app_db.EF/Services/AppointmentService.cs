using AutoMapper;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace medical_app_db.Services;

public class AppointmentService : IAppointmentService
{
    private readonly MedicalDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public AppointmentService(MedicalDbContext context,IHttpContextAccessor contextAccessor,IMapper mapper)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }
    public async Task<IReadOnlyList<Appointment>> GetAppointmentsAsync(DateTime? appointmentDate)
    {
        Guid ClinicId = GetClinicId();

        var appointmetns = await _context.Set<Appointment>()
            .Include(a => a.Clinic)
            .Where(a => a.ClinicId == ClinicId)
            .ToListAsync();

        if (appointmentDate is not null)
            appointmetns = appointmetns.Where(a => a.Date.Date == appointmentDate).ToList();

        return appointmetns;
    }
    public async Task<Appointment?> GetAppointmentAsync(Guid id)
    {
        Guid ClinicId = GetClinicId();

        var appointment = await _context.Set<Appointment>()
            .Where(a => a.ClinicId == ClinicId)
            .Include(a => a.Clinic)
            .SingleOrDefaultAsync(a => a.Id == id);

        return appointment;
    }
    public async Task<bool> AcceptApointment(Guid id)
    {
        Guid ClinicId = GetClinicId();

        var appointment = await _context.Set<Appointment>()
            .SingleOrDefaultAsync(a => a.ClinicId == ClinicId && a.Status == AppointmentStatus.Pending && a.Id == id);

        if (appointment is null)
            return false;

        appointment.Status = AppointmentStatus.Accepted;

        var result = await _context.SaveChangesAsync();

        if (result < 1)
            return false;

        return true;
    }
    public async Task<bool> DeclineApointment(Guid id)
    {
        Guid ClinicId = GetClinicId();

        var appointment = await _context.Set<Appointment>()
            .SingleOrDefaultAsync(a => a.ClinicId == ClinicId && a.Status == AppointmentStatus.Pending && a.Id == id);


        if (appointment is null)
            return false;

        appointment.Status = AppointmentStatus.Decliened;

        var result = await _context.SaveChangesAsync();

        if (result < 1)
            return false;

        return true;
    }

    public async Task<Prescription?> AddPrescriptionAsync(PrescriptionDTO model)
    {
        var doctor = await _context.Set<Doctor>()
            .SingleOrDefaultAsync(d => d.Id == model.DoctorId);

        var appointment = await _context.Set<Appointment>()
            .SingleOrDefaultAsync(a => a.Id == model.AppointmentId);

        if (doctor is null || appointment is null)
            return null;

        var newPrescription = new Prescription()
        {
            Id = Guid.NewGuid(),
            DoctorId = model.DoctorId,
            AppointmentId = model.AppointmentId,
            Tests = model.Tests,
            Diagnosis = model.Diagnosis,
            NextAppointment = model.NextAppointment
        };

        await _context.Set<Prescription>().AddAsync(newPrescription);


        foreach (var item in model.PrescriptionProductDTOs ?? [])
        {
            var prescriptionProduct = new PrescriptionProduct()
            {
                Description = item.Description,
                SystemProductCode = item.SystemProductCode,
                PrescriptionId = newPrescription.Id
            };
            newPrescription.PrescriptionProducts?.Add(prescriptionProduct);

            await _context.Set<PrescriptionProduct>()
                .AddAsync(prescriptionProduct);
        }

        var result = await _context.SaveChangesAsync();

        if (result < 1)
            return null;

        return newPrescription;
    }
    private Guid GetClinicId()
    {
        bool success = Guid.TryParse(_contextAccessor.HttpContext.User
           .FindFirstValue("ClinicId"), out Guid ClinicId);

        if (!success)
            throw new UnauthorizedAccessException("Unothorized to Access This Resourse");

        return ClinicId;
    }

	public async Task<AppointmentDateDTO> UpdateAppointmentDateAsync(Guid id, AppointmentDateDTO appointmentDate)
    {
		Guid ClinicId = GetClinicId();

		var oldAppointmentDate = await _context.AppointmentDates.Include(b => b.WorkingPeriods)
			.FirstOrDefaultAsync(b => b.Id == id && b.ClinicId == ClinicId) ??
			throw new UnauthorizedAccessException("This Appointment date not belongs to your clinc");

		oldAppointmentDate.AppointmentMaxNumber = appointmentDate.AppointmentMaxNumber;
        oldAppointmentDate.Date = appointmentDate.Date;
        oldAppointmentDate.WorkingPeriods = appointmentDate.WorkingPeriods?.Select(w => new WorkingPeriodInClinic
		{
			StartTime = w.StartTime,
            EndTime = w.EndTime,
		}).ToList() ?? new List<WorkingPeriodInClinic>();

		await _context.SaveChangesAsync();

		return _mapper.Map<AppointmentDateDTO>(oldAppointmentDate);
	}

    public async Task<List<AppointmentDateDTO>> GetAppointmentDates()
    {
		Guid ClinicId = GetClinicId();
        var clicnAppointments = await _context.AppointmentDates.Where(ad => ad.ClinicId.Equals(ClinicId)).Select(a => new AppointmentDateDTO
        {
            AppointmentMaxNumber = a.AppointmentMaxNumber,
            Date = a.Date,
            Id = a.Id,
            WorkingPeriods = a.WorkingPeriods.Select(w => new WorkingPeriodInClinicDTO
			{
                StartTime = w.StartTime,
                EndTime = w.EndTime
            }).ToList()
        }).ToListAsync();

        return clicnAppointments;
	}


	private static TimeOnly ParseTime(string timeString)
	{
		try
		{
			var formats = new[] { "h:mm tt", "hh:mm tt", "H:mm tt", "hh:mm tt" };

			if (TimeOnly.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out TimeOnly time))
			{
				return time;
			}
			else
			{
				throw new FormatException("Invalid time format.");
			}
		}
		catch (Exception ex)
		{
			throw new FormatException("Error parsing time: " + ex.Message);
		}
	}
}
