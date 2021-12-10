using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Mails;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface IEmailService
    {
        ResponseResult SendMail(MailViewModel request);
        ResponseResult SendMail(string userName, string email, string password);
    }
}