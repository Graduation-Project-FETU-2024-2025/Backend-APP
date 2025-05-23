﻿using System;
using System.Collections.Generic;

namespace medical_app_db.Core.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string FullName { get; set; } = null!;
		public DateTime Date { get; set; }
        public string State { get; set; } = null!;
		public decimal DeliveryPrice { get; set; }
        public decimal TotalCart { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
		public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public ICollection<Item> Items { get; set; } = null!;
	}
}






