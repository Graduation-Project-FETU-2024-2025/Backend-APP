using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace medical_app_db.Core.DTOs
{
    public class BranchDTO
    {
        public Guid? Id { get; set; }
        public Guid PharmacyId { get; set; }
        public string BranchName { get; set; }
        public string AR_BranchName { get; set; } 
        public string? EN_BranchName { get; set; }
        public int DeliveryRange { get; set; }
        public decimal PricePerKilo { get; set; }
        public decimal MinDeliveryPrice { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string? Address { get; set; }
        public string? AR_Address { get; set; }
        public string? EN_Address { get; set; }
        public IEnumerable<WorkingPeriodDTO>? WorkingHours { get; set; }
    }

    public class WorkingPeriodDTO
    {
        public string Start { get; set; } = null!;
        public string End { get; set; } = null!;
    }
}

