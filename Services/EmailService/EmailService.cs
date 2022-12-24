using employeesServer.Models;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace employeesServer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDTO req)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailUserName"]));
            email.To.Add(MailboxAddress.Parse(req.To));
            email.Subject = req.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = req.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config["EmailHost"], 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["EmailUserName"], _config["EmailPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
