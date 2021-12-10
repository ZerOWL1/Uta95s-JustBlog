using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Mails;
using System.Web.Mvc;
using FA.JustBlog.Common.CommonHelper.CaptchaHelper;

namespace FA.JustBlog.WebUI.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public ActionResult Index(MailViewModel request)
        {
            //Check captcha
            var responseCaptcha = Request.Form["g-recaptcha-response"];
            var isCheckCaptcha = CaptchaHelper.CheckCaptcha(responseCaptcha);

            if (!isCheckCaptcha || string.IsNullOrEmpty(responseCaptcha)) 
            {
                TempData["SendMailFail"] = "Please Check Captcha";
                return RedirectToAction("Contact", "Post");
            }

            //Send mail
            var result = _emailService.SendMail(request);

            if (result.IsSuccess)
            {
                TempData["SendMailSuccess"] = "Send Mail Success!";
                return RedirectToAction("Contact", "Post");
            }

            TempData["SendMailFail"] = "Send Mail Fail!";
            return RedirectToAction("Contact", "Post");
        }
    }
}