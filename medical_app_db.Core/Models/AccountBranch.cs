﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Models
{
	public class AccountBranch
	{
		public Guid AccountId { get; set; }
		public Account Account { get; set; } = null!;

		public Guid BranchId { get; set; }
		public Branch Branch { get; set; } = null!;
	}
}
