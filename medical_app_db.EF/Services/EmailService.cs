using medical_app_db.Core.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using medical_app_db.Core.Helpers;

namespace medical_app_db.EF.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }
        public async Task SendEmailAsync(string? to, string? subject, string? body)
        {
            var smtpClient = new SmtpClient
            {
                EnableSsl = _emailSetting.EnableSsl,
                Host = _emailSetting.Host ?? "smtp.gmail.com",
                Port = _emailSetting.Port,
                Credentials = new NetworkCredential(_emailSetting.Email, _emailSetting.Password)
            };
            var emailMessage = new MailMessage(_emailSetting.Email!, to!, subject, body);

            await smtpClient.SendMailAsync(emailMessage);
        }
    }
}
