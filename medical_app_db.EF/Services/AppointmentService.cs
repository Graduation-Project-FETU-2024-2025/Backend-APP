using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace medical_app_db.Services;

public class AppointmentService : IAppointmentService
{
    private readonly MedicalDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public AppointmentService(MedicalDbContext context,IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }
    public async Task<IReadOnlyList<Appointment>> GetAppointmentsAsync(DateTime? appointmentDate)
    {
        Guid ClinicId = GetClinicId();

        var appointmetns = await _context.Set<Appointment>()
            .Include(a => a.Clinic)
            .Where(a => a.ClinicId == ClinicId)
            .ToListAsync();

        if (appointmentDate is not null)
            appointmetns = appointmetns.Where(a => a.Date == appointmentDate).ToList();

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
            Notes = model.Notes,
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
}
