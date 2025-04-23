using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace medical_app_db.Core.DTOs
{
    public class BranchDTO
    {
        public Guid? Id { get; set; }
        public Guid PharmacyId { get; set; }
        public string? BranchName { get; set; }
        [Required]
        public string? AR_BranchName { get; set; } 
        [Required]
        public string? EN_BranchName { get; set; }
        [Required]
        public int DeliveryRange { get; set; }
        [Required]
        public decimal PricePerKilo { get; set; }
        [Required]
        public decimal MinDeliveryPrice { get; set; }
        [Required]
        public string? Status { get; set; }
        public string? Image { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        public string? Address { get; set; }
        [Required]
        public string? AR_Address { get; set; }
        [Required]
        public string? EN_Address { get; set; }
        public IEnumerable<WorkingPeriodDTO>? WorkingHours { get; set; }
    }

    public class WorkingPeriodDTO
    {
        public string Start { get; set; } = null!;
        public string End { get; set; } = null!;
    }
}

