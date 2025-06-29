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
        var doctors = await _context.Doctors
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic).ThenInclude(c => c.ClinicPhones)
            .Include(d => d.Appointments)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var clinicIds = doctors.Select(d => d.DoctorClinic.ClinicId).ToList();

        var reviews = await _context.Reviews
            .Where(r => clinicIds.Contains(r.ClinicId))
            .ToListAsync();

        var appointmentDates = await _context.AppointmentDates
            .Where(ad => clinicIds.Contains(ad.ClinicId) && ad.Date > DateTime.Now)
            .ToListAsync();

        var items = doctors.Select(d =>
        {
            var clinic = d.DoctorClinic.Clinic;
            var clinicReviews = reviews.Where(r => r.ClinicId == clinic.Id).ToList();
            var rating = clinicReviews.Any() ? clinicReviews.Average(r => r.Rate) : 0;
            var nextDate = appointmentDates
                .Where(ad => ad.ClinicId == clinic.Id)
                .OrderBy(ad => ad.Date)
                .Select(ad => ad.Date.ToString("dddd, hh:mm tt"))
                .FirstOrDefault();

            return new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = clinic.Name,
                ClinicAddress = clinic.Address,
                PhoneNumber = clinic.ClinicPhones.FirstOrDefault()?.PhoneNumber ?? "",
                Rating = rating,
                ReviewsCount = clinicReviews.Count,
                Image = d.Picture,
                Price = clinic.Price,
                About = d.About,
                NextAvailableAppointment = nextDate
            };
        }).ToList();

        var total = await _context.Doctors.CountAsync();

        return new PaginatedResult<DoctorListDto>(items, total);
    }


    public async Task<DoctorListDto?> GetDoctorByIdAsync(Guid id)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic).ThenInclude(c => c.ClinicPhones)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null) return null;

        var clinic = doctor.DoctorClinic.Clinic;

        var reviews = await _context.Reviews
            .Where(r => r.ClinicId == clinic.Id)
            .ToListAsync();

        var rating = reviews.Any() ? reviews.Average(r => r.Rate) : 0;
        var reviewsCount = reviews.Count;

        var nextDate = await _context.AppointmentDates
            .Where(ad => ad.ClinicId == clinic.Id && ad.Date > DateTime.Now)
            .OrderBy(ad => ad.Date)
            .Select(ad => ad.Date.ToString("dddd, hh:mm tt"))
            .FirstOrDefaultAsync();

        return new DoctorListDto
        {
            Id = doctor.Id,
            FullName = doctor.Name,
            ClinicName = clinic.Name,
            ClinicAddress = clinic.Address,
            PhoneNumber = clinic.ClinicPhones.FirstOrDefault()?.PhoneNumber ?? "",
            Rating = rating,
            ReviewsCount = reviewsCount,
            Image = doctor.Picture,
            Price = clinic.Price,
            About = doctor.About,
            NextAvailableAppointment = nextDate
        };
    }


    public async Task<PaginatedResult<DoctorListDto>> GetDoctorsBySpecializationAsync(Guid specializationId, int pageNumber, int pageSize)
    {
        var doctors = await _context.Doctors
            .Where(d => d.SpecializationId == specializationId)
            .Include(d => d.DoctorClinic).ThenInclude(dc => dc.Clinic).ThenInclude(c => c.ClinicPhones)
            .Include(d => d.Appointments)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var clinicIds = doctors.Select(d => d.DoctorClinic.ClinicId).ToList();

        var reviews = await _context.Reviews
            .Where(r => clinicIds.Contains(r.ClinicId))
            .ToListAsync();

        var appointmentDates = await _context.AppointmentDates
            .Where(ad => clinicIds.Contains(ad.ClinicId) && ad.Date > DateTime.Now)
            .ToListAsync();

        var items = doctors.Select(d =>
        {
            var clinic = d.DoctorClinic.Clinic;
            var clinicReviews = reviews.Where(r => r.ClinicId == clinic.Id).ToList();
            var rating = clinicReviews.Any() ? clinicReviews.Average(r => r.Rate) : 0;
            var nextDate = appointmentDates
                .Where(ad => ad.ClinicId == clinic.Id)
                .OrderBy(ad => ad.Date)
                .Select(ad => ad.Date.ToString("dddd, hh:mm tt"))
                .FirstOrDefault();

            return new DoctorListDto
            {
                Id = d.Id,
                FullName = d.Name,
                ClinicName = clinic.Name,
                ClinicAddress = clinic.Address,
                PhoneNumber = clinic.ClinicPhones.FirstOrDefault()?.PhoneNumber ?? "",
                Rating = rating,
                ReviewsCount = clinicReviews.Count,
                Image = d.Picture,
                Price = clinic.Price,
                About = d.About,
                NextAvailableAppointment = nextDate
            };
        }).ToList();

        var total = await _context.Doctors.CountAsync(d => d.SpecializationId == specializationId);

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
        var doctors = await GetDoctorsBySpecializationAsync(specializationId, pageNumber, pageSize);
        var sorted = doctors.Items.OrderByDescending(d => d.Rating).ToList();

        return new PaginatedResult<DoctorListDto>(sorted, doctors.TotalCount);
    }


}
