using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Infrastructure.Settings;
using System.Net;
using System.Net.Mail;

namespace ShipperStation.Infrastructure.Services
{
    public class EmailSender(IOptions<MailSettings> mailSettings) : IEmailSender
    {
        private readonly MailSettings _mailSettings = mailSettings.Value;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = _mailSettings.Port,
                Host = _mailSettings.Host,
                EnableSsl = _mailSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _mailSettings.UseDefaultCredentials,
                Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.From),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = _mailSettings.IsBodyHtml,
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
