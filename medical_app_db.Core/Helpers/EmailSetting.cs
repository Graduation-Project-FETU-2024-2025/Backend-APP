﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Helpers
{
    public class EmailSetting
    {
        public int Port { get; set; }
        public string? Host { get; set; }
        public bool EnableSsl { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
