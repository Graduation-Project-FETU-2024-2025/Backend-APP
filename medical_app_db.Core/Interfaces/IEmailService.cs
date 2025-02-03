using medical_app_db.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string? to, string? subject, string? body);
    }
}
