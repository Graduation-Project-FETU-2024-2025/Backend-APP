using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;

public class DoctorService : IDoctorService
{
    private readonly MedicalDbContext _context;

    public DoctorService(MedicalDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<DoctorListDto>> GetAllDoctorsAsync(int pageNumber, int pageSize)
    {
        var query = _context.Doctors
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic)
            .Include(d => d.Appointments)
            .Select(d => new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = d.DoctorClinic.Clinic.Name,
                ClinicAddress = d.DoctorClinic.Clinic.Address,
                PhoneNumber = d.DoctorClinic.Clinic.ClinicPhones.FirstOrDefault().PhoneNumber, 
                Rating = _context.Reviews
                    .Where(r => r.ClinicId == d.DoctorClinic.ClinicId)
                    .Average(r => (double?)r.Rate) ?? 0,

                ReviewsCount = _context.Reviews
                    .Count(r => r.ClinicId == d.DoctorClinic.ClinicId),
                Image= d.Picture,
                NextAvailableAppointment = d.Appointments
                    .Where(a => a.Date > DateTime.Now)
                    .OrderBy(a => a.Date)
                    .Select(a => a.Date.ToString("dddd, hh:mm tt"))
                    .FirstOrDefault()
            });

        var total = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<DoctorListDto>(items, total);
    }

    public async Task<DoctorListDto?> GetDoctorByIdAsync(Guid id)
    {
        return await _context.Doctors
            .Where(d => d.Id == id)
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic)
            .Select(d => new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = d.DoctorClinic.Clinic.Name,
                ClinicAddress = d.DoctorClinic.Clinic.Address,
                PhoneNumber = d.DoctorClinic.Clinic.ClinicPhones.FirstOrDefault().PhoneNumber,
                Rating = _context.Reviews
                    .Where(r => r.ClinicId == d.DoctorClinic.ClinicId)
                    .Average(r => (double?)r.Rate) ?? 0,
                ReviewsCount = _context.Reviews
                    .Count(r => r.ClinicId == d.DoctorClinic.ClinicId),
                Image = d.Picture,
                NextAvailableAppointment = d.Appointments
                    .Where(a => a.Date > DateTime.Now)
                    .OrderBy(a => a.Date)
                    .Select(a => a.Date.ToString("dddd, hh:mm tt"))
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PaginatedResult<DoctorListDto>> GetDoctorsBySpecializationAsync(Guid specializationId, int pageNumber, int pageSize)
    {
        var query = _context.Doctors
            .Where(d => d.SpecializationId == specializationId)
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic)
            .Include(d => d.Appointments)
            .Select(d => new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = d.DoctorClinic.Clinic.Name,
                Rating = _context.Reviews
                    .Where(r => r.ClinicId == d.DoctorClinic.ClinicId)
                    .Average(r => (double?)r.Rate) ?? 0,
                ReviewsCount = _context.Reviews
                    .Count(r => r.ClinicId == d.DoctorClinic.ClinicId),
                NextAvailableAppointment = d.Appointments
                    .Where(a => a.Date > DateTime.Now)
                    .OrderBy(a => a.Date)
                    .Select(a => a.Date.ToString("dddd, hh:mm tt"))
                    .FirstOrDefault()
            });

        var total = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<DoctorListDto>(items, total);
    }

    public async Task<PaginatedResult<DoctorListDto>> GetTopRatedDoctorsAsync(int pageNumber, int pageSize)
    {
        var doctors = await GetAllDoctorsAsync(pageNumber, pageSize);
        var sorted = doctors.Items.OrderByDescending(d => d.Rating).ToList();

        return new PaginatedResult<DoctorListDto>(sorted, doctors.TotalCount);
    }

    public async Task<PaginatedResult<DoctorListDto>> GetTopRatedDoctorsBySpecializationAsync(Guid specializationId, int pageNumber, int pageSize)
    {
        var query = _context.Doctors
            .Where(d => d.SpecializationId == specializationId)
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic)
            .Include(d => d.Appointments)
            .Select(d => new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = d.DoctorClinic.Clinic.Name,
                ClinicAddress = d.DoctorClinic.Clinic.Address,
                PhoneNumber = d.DoctorClinic.Clinic.ClinicPhones.FirstOrDefault().PhoneNumber,
                Image = d.Picture,
                Rating = _context.Reviews
                    .Where(r => r.ClinicId == d.DoctorClinic.ClinicId)
                    .Average(r => (double?)r.Rate) ?? 0,
                ReviewsCount = _context.Reviews
                    .Count(r => r.ClinicId == d.DoctorClinic.ClinicId),
                NextAvailableAppointment = d.Appointments
                    .Where(a => a.Date > DateTime.Now)
                    .OrderBy(a => a.Date)
                    .Select(a => a.Date.ToString("dddd, hh:mm tt"))
                    .FirstOrDefault()
            });

        var total = await query.CountAsync();

       
        var items = await query
            .OrderByDescending(d => d.Rating)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<DoctorListDto>(items, total);
    }

}
