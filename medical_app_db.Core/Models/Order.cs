using System;
using System.Collections.Generic;

namespace medical_app_db.Core.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalCart { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        //public int BranchId { get; set; }
        //public Branch Branch { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}






