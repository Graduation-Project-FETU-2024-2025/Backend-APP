using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using medical_app_db.EF.Migrations;
namespace medical_app_db.Services;
public class BranchService : IBranchService
{
    private readonly MedicalDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageService _imageService;

    public BranchService(MedicalDbContext context, IHttpContextAccessor httpContextAccessor, IImageService imageService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _imageService = imageService;
    }

    public async Task<IEnumerable<BranchDTO>> GetAllBranchesAsync(string lang, int page = 1, int pageSize = 3)
    {
        var pharmacyId = (Guid)_httpContextAccessor.HttpContext.Items["PharmacyId"];
        var branchesId = await GetAccountBranchIds();

        var branches = await _context.Branches
            .Where(b => b.PharmacyId == pharmacyId && branchesId.Contains(b.Id))
            .Include(b => b.WorkingPeriods)
            .Select(b => new BranchDTO
            {
                Id = b.Id,
                BranchName = lang == "ar" ? b.AR_BranchName : b.EN_BranchName,
                AR_BranchName = b.AR_BranchName,
                EN_BranchName = b.EN_BranchName,
                PhoneNumber = b.PhoneNumber,
                Image = b.Image,
                Status = b.Status,
                Lat = b.Lat,
                Long = b.Long,
                Address = lang == "ar" ? b.AR_Address : b.EN_Address,
                DeliveryRange = b.DeliveryRange,
                PricePerKilo = b.PricePerKilo,
                MinDeliveryPrice = b.MinDeliveryPrice,
                WorkingHours = b.WorkingPeriods != null
                    ? b.WorkingPeriods.Select(w => new WorkingPeriodDTO
                    {
                        Start = w.Start.ToString("hh:mm tt"),
                        End = w.End.ToString("hh:mm tt")
                    }).ToList()
                    : new List<WorkingPeriodDTO>()
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return branches;
    }

    public async Task<BranchDTO> GetBranchByIdAsync(Guid id, string lang)
    {
        var pharmacyId = (Guid)_httpContextAccessor.HttpContext.Items["PharmacyId"];

        var branch = await _context.Branches.Include(b => b.WorkingPeriods)
            .FirstOrDefaultAsync(b => b.Id == id && b.PharmacyId == pharmacyId);

        if (branch == null)
            throw new KeyNotFoundException("Branch not found");

        return new BranchDTO
        {
            Id = branch.Id,
            BranchName = lang == "ar" ? branch.AR_BranchName : branch.EN_BranchName, 
            AR_BranchName = branch.AR_BranchName, 
            EN_BranchName = branch.EN_BranchName,
            Lat = branch.Lat,
            Long = branch.Long,
            Address = lang == "ar" ? branch.AR_Address : branch.EN_Address,
            Status = branch.Status,
            DeliveryRange = branch.DeliveryRange,
            PricePerKilo = branch.PricePerKilo,
            MinDeliveryPrice = branch.MinDeliveryPrice,
            Image = branch.Image,
            PhoneNumber = branch.PhoneNumber,
            WorkingHours = branch.WorkingPeriods?.Select(w => new WorkingPeriodDTO
            {
                Start = w.Start.ToString("hh:mm tt"),
                End = w.End.ToString("hh:mm tt")
            }).ToList()
        };
    }

    private async Task<IReadOnlyList<Guid>> GetAccountBranchIds()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        _ = Guid.TryParse(httpContext.User.FindFirst("AccountId")?.Value, out Guid accountId);

        var branchesId = await _context.AccountBranches
            .Where(ab => ab.AccountId == accountId)
            .Select(ab => ab.BranchId)
            .ToListAsync();

        return branchesId;
    }
    public async Task<BranchDTO> AddBranchAsync(BranchDTO branchDto, IFormFile? image)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        _ = Guid.TryParse(httpContext.User.FindFirst("Account")?.Value, out Guid accountId);
        try
        {
            ValidateBranchData(branchDto);

            var pharmacyId = (Guid)_httpContextAccessor.HttpContext.Items["PharmacyId"];
            if (branchDto.PharmacyId != pharmacyId)
                throw new UnauthorizedAccessException("Unauthorized to add a Branch Ti this Pharmacy");

            var branchId = Guid.NewGuid();

            var branch = new Branch
            {
                Id = branchId,
                PharmacyId = pharmacyId,
                AR_BranchName = branchDto.AR_BranchName,
                EN_BranchName = branchDto.EN_BranchName,
                Lat = branchDto.Lat,
                Long = branchDto.Long,
                AR_Address = branchDto.AR_Address,
                EN_Address = branchDto.EN_Address,
                DeliveryRange = branchDto.DeliveryRange,
                PricePerKilo = branchDto.PricePerKilo,
                MinDeliveryPrice = branchDto.MinDeliveryPrice,
                Status = branchDto.Status,
                Image = await _imageService.UploadImageAsync(image, branchId),
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

            await _context.Branches.AddAsync(branch);

            var accountBrnach = new AccountBranch()
            {
                AccountId = accountId,
                BranchId = branch.Id
            };
            await _context.AccountBranches.AddAsync(accountBrnach);

            await _context.SaveChangesAsync();

            branchDto.Id = branch.Id;
            branchDto.Image = branch.Image;
            return branchDto;
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
    public async Task<BranchDTO> UpdateBranchAsync(Guid id, BranchDTO branchDto, IFormFile? image)
    {
        var pharmacyId = (Guid)_httpContextAccessor.HttpContext.Items["PharmacyId"];
        var branch = await _context.Branches.Include(b => b.WorkingPeriods)
            .FirstOrDefaultAsync(b => b.Id == id && b.PharmacyId == pharmacyId);

        if (branch == null)
            throw new UnauthorizedAccessException("Branch not found or does not belong to the pharmacy.");

        ValidateBranchData(branchDto);

        branch.AR_BranchName = branchDto.AR_BranchName;
        branch.EN_BranchName = branchDto.EN_BranchName;
        branch.Lat = branchDto.Lat;
        branch.Long = branchDto.Long;
        branch.DeliveryRange = branchDto.DeliveryRange;
        branch.PricePerKilo = branchDto.PricePerKilo;
        branch.MinDeliveryPrice = branchDto.MinDeliveryPrice;
        branch.Status = branchDto.Status;

        if (branch.Image != branchDto.Image)
        {
            branch.Image = await _imageService.UpdateImageAsync(image,branch.Id);
        }

        branch.PhoneNumber = branchDto.PhoneNumber;
        branch.WorkingPeriods = branchDto.WorkingHours?.Select(w => new WorkingPeriod
        {
            Start = ParseTime(w.Start),
            End = ParseTime(w.End)
        }).ToList();
        branch.AR_Address = branchDto.AR_Address;
        branch.EN_Address = branchDto.EN_Address;

        await _context.SaveChangesAsync();
        branchDto.Id = id;
        branchDto.Image = branch.Image;
        return branchDto;
    }
    public async Task<bool> DeleteBranchAsync(Guid branchId)
    {
        var pharmacyId = (Guid)_httpContextAccessor.HttpContext.Items["PharmacyId"];

        var branch = await _context.Branches
            .Where(b => b.PharmacyId == pharmacyId)
            .FirstOrDefaultAsync(b => b.Id == branchId);

        if (branch == null)
            return false;

        var accountBranches = await _context.AccountBranches
            .Where(ac => ac.BranchId == branchId)
            .ToListAsync();

        foreach (var accountBranch in accountBranches)
        {
            _context.AccountBranches.Remove(accountBranch);
        }

        await _imageService.DeleteImageAsync(branch.Id);

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();
        return true;
    }
    private void ValidateBranchData(BranchDTO branchDto)
    {
        if (!Regex.IsMatch(branchDto.AR_BranchName, @"^[\u0600-\u06FF\s]+$"))
        {
            throw new ArgumentException("AR_BranchName must contain only Arabic letters.");
        }

        if (!Regex.IsMatch(branchDto.EN_BranchName, @"^[A-Za-z\s]+$"))
        {
            throw new ArgumentException("EN_BranchName must contain only English letters.");
        }

        if (!Regex.IsMatch(branchDto.PhoneNumber, @"^\d+$"))
        {
            throw new ArgumentException("PhoneNumber must contain only digits.");
        }

        if (branchDto.Lat < -90 || branchDto.Lat > 90)
        {
            throw new ArgumentException("Latitude must be between -90 and 90.");
        }

        if (branchDto.Long < -180 || branchDto.Long > 180)
        {
            throw new ArgumentException("Longitude must be between -180 and 180.");
        }

        if (branchDto.WorkingHours != null)
        {
            foreach (var workHour in branchDto.WorkingHours)
            {
                try
                {
                    TimeOnly startTime = ParseTime(workHour.Start);
                    TimeOnly endTime = ParseTime(workHour.End);

                    if (startTime >= endTime)
                    {
                        throw new ArgumentException("Start time must be earlier than end time.");
                    }
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Invalid working hours format: " + ex.Message);
                }
            }
        }
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


}
