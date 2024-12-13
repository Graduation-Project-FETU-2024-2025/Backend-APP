using System;

namespace medical_app_db.Core.Models
{
    public class Item
    {
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        //public Guid Code { get; set; }
        //public SystemProduct SystemProduct { get; set; }
    }
}



