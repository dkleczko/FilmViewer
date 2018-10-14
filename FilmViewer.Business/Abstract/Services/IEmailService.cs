using FilmViewer.Business.Dto.Extended.Email;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IEmailService
    {
        void SendEmailMessage(EmailMessageDto emailMessage);
    }
}
