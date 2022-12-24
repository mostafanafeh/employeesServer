using employeesServer.Models;

namespace employeesServer.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO req);

    }
}
