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
    private readonly IImageService _imageService;

    public AppointmentService(MedicalDbContext context,
        IHttpContextAccessor contextAccessor,
        IMapper mapper,
        IImageService imageService)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _imageService = imageService;
    }
    public async Task<IReadOnlyList<AppointmentDTO>> GetAppointmentsAsync(DateTime? appointmentDate, AppointmentStatus? status, AppointmentType? type)
    {
        Guid ClinicId = GetClinicId();

        var appointmetns = await _context.Set<Appointment>()
            .Include(a => a.Clinic)
            .Include(a => a.User)
            .Where(a => a.ClinicId == ClinicId)
            .ToListAsync();

        if (appointmentDate is not null)
            appointmetns = appointmetns.Where(a => a.Date.Date == appointmentDate).ToList();

        if (status is not null)
            appointmetns = appointmetns.Where(a => a.Status == status).ToList();

        if (type is not null)
            appointmetns = appointmetns.Where(a => a.Type == type).ToList();



        return _mapper.Map<IReadOnlyList<AppointmentDTO>>(appointmetns);
    }
	public async Task<IReadOnlyList<AppointmentDTO>> GetUserAppointmentsAsync(DateTime? appointmentDate, AppointmentStatus? status, AppointmentType? type, Guid user_id)
	{
		var appointmetns = await _context.Set<Appointment>()
            .Where(a => a.UserId == user_id)
            .Select(a => new AppointmentDTO
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status.ToString(),
                ClinicId = a.ClinicId,
                ClinicName = a.Clinic.Name,
                UserId = a.UserId,
                UserName = a.User.Name, // Make sure `FullName` exists
                DoctorName = a.DoctorName,
                Price = a.Price,
                Type = a.Type.ToString(),
                Complaint = a.Complaint,
                UserImage = a.User.Picture,
                FileUrl = a.FileUrl
            })
            .ToListAsync();

        if (appointmentDate is not null)
			appointmetns = appointmetns.Where(a => a.Date.Date == appointmentDate).ToList();

		if (status is not null)
            appointmetns = appointmetns.Where(a => a.Status == status.ToString()).ToList();

        if (type is not null)
            appointmetns = appointmetns.Where(a => a.Type == type.ToString()).ToList();



        return _mapper.Map<IReadOnlyList<AppointmentDTO>>(appointmetns);
	}
	public async Task<IReadOnlyList<AppointmentDTO>> GetUserInCompleteAppointmentsAsync(DateTime? appointmentDate, AppointmentType? type, Guid user_id)
	{

        var appointmetns = await _context.Set<Appointment>()
			.Where(a => a.UserId == user_id && (a.Status == AppointmentStatus.Pending || a.Status == AppointmentStatus.Accepted))
            .Select(a => new AppointmentDTO
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status.ToString(),
                ClinicId = a.ClinicId,
                ClinicName = a.Clinic.Name,
                UserId = a.UserId,
                UserName = a.User.Name, // Make sure `FullName` exists
                DoctorName = a.DoctorName,
                Price = a.Price,
                Type = a.Type.ToString(),
                Complaint = a.Complaint,
                UserImage = a.User.Picture,
                FileUrl = a.FileUrl
            })
            .ToListAsync();

        if (appointmentDate is not null)
			appointmetns = appointmetns.Where(a => a.Date.Date == appointmentDate).ToList();

		if (type is not null)
			appointmetns = appointmetns.Where(a => a.Type == type.ToString()).ToList();



		return _mapper.Map<IReadOnlyList<AppointmentDTO>>(appointmetns);
	}
	public async Task<AppointmentDTO?> GetAppointmentAsync(Guid id)
    {
        Guid ClinicId = GetClinicId();

        var appointment = await _context.Set<Appointment>()
            .Where(a => a.ClinicId == ClinicId)
            .Include(a => a.Clinic)
            .Include(a => a.User)
            .SingleOrDefaultAsync(a => a.Id == id);

        return _mapper.Map<AppointmentDTO>(appointment);
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
        if (model.DoctorId == Guid.Empty)
            throw new ArgumentNullException(nameof(model.DoctorId), "DoctorId is required");
        try
        {
            var doctor = await _context?.Set<Doctor>()?
                .FirstOrDefaultAsync(d => d.Id == model.DoctorId);
        }catch(Exception ex)
        {
            throw (new Exception($"Received DoctorId: {model.DoctorId}"));
        }

        var appointment = await _context.Set<Appointment>()
            .SingleOrDefaultAsync(a => a.Id == model.AppointmentId);

        //if (doctor is null || appointment is null)
        //    return null;

        var newPrescription = new Prescription()
        {
            Id = Guid.NewGuid(),
            DoctorId = model.DoctorId,
            AppointmentId = model.AppointmentId,
            Tests = model.Tests,
            Diagnosis = model.Diagnosis,
            NextAppointment = model.NextAppointment,
            PrescriptionProducts = new List<PrescriptionProduct>()
        };
        try
        {


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
        }
        catch (Exception ex)
        {
            throw (new Exception(ex.Message, ex));
        }
        var result = 0;
        try
        {
            result = await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw (new Exception(ex.Message, ex));
        }

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
	public async Task<AppointmentDateDTO> AddAppointmentDateAsync(AppointmentDateDTO appointmentDate)
    {
		Guid ClinicId = GetClinicId();

		var oldAppointmentDate = await _context.AppointmentDates
            .Where(ad => ad.ClinicId.Equals(ClinicId))
            .Include(ad => ad.WorkingPeriods)
            .FirstOrDefaultAsync();
        var newAppointmentdate = new AppointmentDates();
        if(oldAppointmentDate is null)
        {
            newAppointmentdate = new AppointmentDates
            {
                Id = Guid.NewGuid(),
                ClinicId = ClinicId,
                AppointmentMaxNumber = appointmentDate.AppointmentMaxNumber,
                Date = appointmentDate.Date,
                WorkingPeriods = appointmentDate.WorkingPeriods?.Select(w => new WorkingPeriodInClinic
                {
                    StartTime = w.StartTime,
                    EndTime = w.EndTime,
                }).ToList() ?? new List<WorkingPeriodInClinic>()
            };
            await _context.AppointmentDates.AddAsync(newAppointmentdate);
        }
        else
        {
            oldAppointmentDate.AppointmentMaxNumber = appointmentDate.AppointmentMaxNumber;
            oldAppointmentDate.Date = appointmentDate.Date;
            oldAppointmentDate.WorkingPeriods = appointmentDate.WorkingPeriods?.Select(w => new WorkingPeriodInClinic
            {
                StartTime = w.StartTime,
                EndTime = w.EndTime,
            }).ToList() ?? new List<WorkingPeriodInClinic>();
            _context.AppointmentDates.Update(oldAppointmentDate);
        }
        newAppointmentdate = oldAppointmentDate ?? newAppointmentdate;
        await _context.SaveChangesAsync();

		return _mapper.Map<AppointmentDateDTO>(oldAppointmentDate);
	}
    public async Task<List<AppointmentDateDTO>> GetAppointmentDates(Guid clinicId)
    {
        var clicnAppointments = await _context.AppointmentDates.Where(ad => ad.ClinicId.Equals(clinicId)).Select(a => new AppointmentDateDTO
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
	public async Task<AppointmentDTO> createAppointmentAsync(AppointmentDTO appointmentDTO,IFormFile? file)
	{
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Id == appointmentDTO.UserId);

        if (user is null)
            throw new Exception("User not found.");

        var clinic = await _context.Set<Clinic>()
            .Where(c => c.Id == appointmentDTO.ClinicId)
            .FirstOrDefaultAsync();

        if (clinic is null)
            throw new Exception("Clinic not found.");
       
        try
		{

			var appointmentId = Guid.NewGuid();
            
            var appointment = new Appointment
			{
			    Id = appointmentId,
	            Date = appointmentDTO.Date,
	            Status = AppointmentStatus.Pending,
	            Type = Enum.Parse<AppointmentType>(appointmentDTO.Type),
	            ClinicId = appointmentDTO.ClinicId,
	            UserId = user.Id,
	            UserName = user.UserName,
	            DoctorName = appointmentDTO.DoctorName,
	            Price = clinic.Price,
	            Complaint = appointmentDTO.Complaint
            };
            if (file is not null)
            {
                var fileUrl = await _imageService.UploadImageAsync(file, appointmentId);
                appointment.FileUrl = fileUrl;
            }

            await _context.Appointments.AddAsync(appointment);

			await _context.SaveChangesAsync();

            appointmentDTO.Id = appointment.Id;

			return _mapper.Map<AppointmentDTO>(appointment);
		}
		catch (ArgumentException argEx)
		{
			throw new Exception("Validation error: " + argEx.Message);
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while adding branch: " + ex);
		}
	}

	public async Task<bool> deleteAppointmentAsync(Guid id)
	{
		var httpContext = _contextAccessor.HttpContext;
		_ = Guid.TryParse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId);

		var appointment = await _context.Set<Appointment>()
			.Where(a => a.Id == id)
			.FirstAsync();

        if(appointment.UserId != userId)
            return false;


		_context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
	}

	public async Task<AppointmentDTO> updateAppointmentAsync(AppointmentDTO appointmentDTO)
	{
		var appointment = await _context.Appointments
				.FirstOrDefaultAsync(a => a.Id == appointmentDTO.Id);
        if (appointment.UserId != appointmentDTO.UserId)
            return null;

        appointment.Date = appointmentDTO.Date;
        appointment.Complaint = appointmentDTO.Complaint;

		await _context.SaveChangesAsync();
		return appointmentDTO;
	}
}
