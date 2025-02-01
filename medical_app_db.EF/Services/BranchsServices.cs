using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
namespace medical_app_db.Services;

public class BranchService : IBranchService
{
    private readonly MedicalDbContext _context;

    public BranchService(MedicalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BranchDTO>> GetAllBranchesAsync()
    {
        var branches = await _context.Branches
            .Include(b => b.WorkingPeriods)
            .Select(b => new BranchDTO
            {
                Id = b.Id,
                AR_BranchName = b.AR_BranchName,
                EN_BranchName = b.EN_BranchName,
                PhoneNumber = b.PhoneNumber,
                Image = b.Image,
                Status = b.Status,
                Lat = b.Lat,
                Long = b.Long,
                WorkingHours = b.WorkingPeriods != null
                    ? b.WorkingPeriods.Select(w => new WorkingPeriodDTO
                    {
                        Start = w.Start.ToString("hh:mm tt"), // التغيير هنا ليتناسب مع AM/PM
                        End = w.End.ToString("hh:mm tt")
                    }).ToList()
                    : new List<WorkingPeriodDTO>()
            })
            .ToListAsync();

        return branches;
    }

    public async Task<BranchDTO> GetBranchByIdAsync(Guid id, string lang)
    {
        var branch = await _context.Branches.Include(b => b.WorkingPeriods).FirstOrDefaultAsync(b => b.Id == id);
        if (branch == null)
            throw new KeyNotFoundException("Branch not found");

        return new BranchDTO
        {
            Id = branch.Id,
            AR_BranchName = lang == "ar" ? branch.AR_BranchName : branch.EN_BranchName,
            EN_BranchName = lang == "en" ? branch.EN_BranchName : branch.AR_BranchName,
            Lat = branch.Lat,
            Long = branch.Long,
            DeliveryRange = branch.DeliveryRange,
            PricePerKilo = branch.PricePerKilo,
            MinDeliveryPrice = branch.MinDeliveryPrice,
            Status = branch.Status,
            Image = branch.Image,
            PhoneNumber = branch.PhoneNumber,
            WorkingHours = branch.WorkingPeriods?.Select(w => new WorkingPeriodDTO
            {
                Start = w.Start.ToString("hh:mm tt"),
                End = w.End.ToString("hh:mm tt")
            }).ToList()
        };
    }

    public async Task<BranchDTO> AddBranchAsync(BranchDTO branchDto)
    {
        try
        {
            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                PharmacyId = branchDto.PharmacyId,
                AR_BranchName = branchDto.AR_BranchName,
                EN_BranchName = branchDto.EN_BranchName,
                Lat = branchDto.Lat,
                Long = branchDto.Long,
                DeliveryRange = branchDto.DeliveryRange,
                PricePerKilo = branchDto.PricePerKilo,
                MinDeliveryPrice = branchDto.MinDeliveryPrice,
                Status = branchDto.Status,
                Image = branchDto.Image,
                PhoneNumber = branchDto.PhoneNumber,
                WorkingPeriods = branchDto.WorkingHours?.Select(w =>
                {

                    TimeOnly startTime = ParseTime(w.Start);
                    TimeOnly endTime = ParseTime(w.End);

                    return new WorkingPeriod
                    {
                        Start = startTime,
                        End = endTime
                    };
                }).ToList()
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            branchDto.Id = branch.Id;
            return branchDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while adding branch: " + ex.Message);
        }
    }

    public async Task<BranchDTO> UpdateBranchAsync(Guid id, BranchDTO branchDto)
    {
        var branch = await _context.Branches.Include(b => b.WorkingPeriods).FirstOrDefaultAsync(b => b.Id == id);
        if (branch == null)
            return null;

        branch.AR_BranchName = branchDto.AR_BranchName;
        branch.EN_BranchName = branchDto.EN_BranchName;
        branch.Lat = branchDto.Lat;
        branch.Long = branchDto.Long;
        branch.DeliveryRange = branchDto.DeliveryRange;
        branch.PricePerKilo = branchDto.PricePerKilo;
        branch.MinDeliveryPrice = branchDto.MinDeliveryPrice;
        branch.Status = branchDto.Status;
        branch.Image = branchDto.Image;
        branch.PhoneNumber = branchDto.PhoneNumber;
        branch.WorkingPeriods = branchDto.WorkingHours?.Select(w =>
        {
            TimeOnly startTime = ParseTime(w.Start);
            TimeOnly endTime = ParseTime(w.End);

            return new WorkingPeriod
            {
                Start = startTime,
                End = endTime
            };
        }).ToList();

        await _context.SaveChangesAsync();
        return branchDto;
    }

    private TimeOnly ParseTime(string timeString)
    {
        try
        {
            TimeOnly time;
            var formats = new[] { "h:mm tt", "hh:mm tt", "H:mm tt", "hh:mm tt" };

            if (TimeOnly.TryParseExact(timeString, formats, null, System.Globalization.DateTimeStyles.None, out time))
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

    public async Task<bool> DeleteBranchAsync(Guid branchId)
    {
        var branch = await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == branchId);

        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();
        return true;
    }
}