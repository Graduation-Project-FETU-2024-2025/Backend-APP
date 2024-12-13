using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace medical_app_db.Core.Models
{
    [PrimaryKey("OrderId", "Code")] 
    public class Item
    {
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public Guid Code { get; set; }

        [ForeignKey(nameof(Code))]
        public SystemProduct SystemProduct { get; set; }
    }
}



