using System.Threading.Tasks;

namespace EmailService
{
    interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}