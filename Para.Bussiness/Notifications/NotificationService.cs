using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Para.Bussiness.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly SmtpSettings _smtpSettings;

        public NotificationService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(string subject, string email, string content)
        {
            SmtpClient mySmtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("kadikoy_fener@hotmail.com", "MHTKLgrz1453")
            };

            MailAddress from = new MailAddress("kadikoy_fener@hotmail.com", "Company");
            MailAddress to = new MailAddress(email);
            MailMessage myMail = new MailMessage(from, to)
            {
                Subject = subject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = content,
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true
            };

            mySmtpClient.Send(myMail);
        }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
