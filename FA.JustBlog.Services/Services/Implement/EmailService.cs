using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Mails;
using System;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Helpers;

namespace FA.JustBlog.Services.Services.Implement
{
    public class EmailService : IEmailService
    {
        public ResponseResult SendMail(MailViewModel request)
        {
            try
            {
                var smtpSection = (SmtpSection)ConfigurationManager
                    .GetSection("system.net/mailSettings/smtp");

                var toMail = smtpSection.Network.UserName;

                var subject = $"Contact UTA95S JustBlog - {request.Name.Trim().ToUpper()}";

                var body =
                    "DEAR Verification Team - UTA95S, <br/>" +
                    $"- WROTE: {request.Name}<br/>" +
                    $"- CONTACT: {request.Email}<br/>" +
                    $"- PHONE: {request.Phone}<br/>" +
                    $"- MESSAGES: <br/>" +
                    $"{request.Message}<br/>";

                WebMail.Send(toMail, subject, body);

                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult SendMail(string userName, string email, string password)
        {
            try
            {
                var subject = $"{userName.Trim().ToUpper()} Account - UTA95S Verification Team - NO REPLY";
                var body =
                    $"DEAR User {userName}, <br/>" +
                    $"- NOTE: You've register in our website - here is account information: <br/>" +
                    $"- USERNAME: {userName}<br/>" +
                    $"- EMAIL: {email}<br/>" +
                    $"- PASSWORD: {password}<br/>" +
                    $"- MESSAGES: <br/>" +
                    $"You must sign in using username and password<br/>";

                WebMail.Send(email, subject, body);

                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }
    }
}