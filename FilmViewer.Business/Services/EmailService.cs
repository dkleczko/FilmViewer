using System.Net;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Extended.Email;

namespace FilmViewer.Business.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmailMessage(EmailMessageDto emailMessage)
        {
            var fromAddress = "ToFill";
            var toAddress = "ToFill";
            const string fromPassword = "ToFill";
            var subject = emailMessage.ClientName;
            var body = string.Format("From: {0} \n Email: {1} \n Subject: {2}", emailMessage.ClientName,
                emailMessage.EmailAddress, emailMessage.Message);
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            smtp.Send(fromAddress, toAddress, subject, body);
        }
    }
}
