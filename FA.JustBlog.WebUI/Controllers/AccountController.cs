using FA.JustBlog.Common.CommonHelper.CaptchaHelper;
using FA.JustBlog.Common.CommonHelper.PasswordHelper;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Core.Models.Enums;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.String;

namespace FA.JustBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public AccountController(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public AccountController(ApplicationUserManager userManager
            , ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext()
                .Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        // GET: Account
        [Route("")]
        public async Task<ActionResult> SignIn()
        {
            //Auto sign in if user cookie already exist
            var loginCookie = Request.Cookies["USER_REQUEST"];
            if (loginCookie == null)
            {
                return View();
            }

            var userName = loginCookie.Values["REQUEST_USERNAME"];
            var password = loginCookie.Values["REQUEST_PASSWORD"];

            var user = await UserManager.FindByNameAsync(userName);

            if (user != null)
            {
                //Check user status
                if (user.Status == Status.IsUnPublished)
                {
                    ViewBag.Message = "Your Account Get Banned or Disabled";
                    return View();
                }

                var role = GetUserRole(user.Id);

                if (password != null)
                {
                    //sign in with user account
                    var resultSignIn = await SignInManager.PasswordSignInAsync(userName,
                        password, true, false);

                    if (resultSignIn == SignInStatus.Success)
                    {
                        AddSession(role, user);
                        return RedirectToAction("Index", "Admin", new { Area = "Admin" });
                    }
                }
                else
                {
                    //sign in with external - google account
                    var external = loginCookie.Values["REQUEST_EXTERNAL"];
                    if (string.Equals(user.External, external, 
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        AddSession(role, user);
                        return RedirectToAction("Index", "Admin", new { Area = "Admin" });
                    }
                }
            }

            ViewBag.Message = "Error while logging in!";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel request)
        {
            //CHECK MODEL_STATE
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            //CHECK CAPTCHA
            var responseCaptcha = Request.Form["g-recaptcha-response"];
            var isCheckCaptcha = CaptchaHelper.CheckCaptcha(responseCaptcha);

            if (!isCheckCaptcha || IsNullOrEmpty(responseCaptcha)) 
            {
                ViewBag.Message = "Please Check Captcha";
                return View();
            }

            //USER CONTROL
            var user = await UserManager.FindByNameAsync(request.UserName);

            if (user.Status == Status.IsUnPublished)
            {
                ViewBag.Message = "Your Account Get Banned or Disabled";
                return View();
            }

            var resultSignIn = await SignInManager.PasswordSignInAsync(request.UserName,
                request.Password, true, false);

            if (SignInStatus.Success == resultSignIn)
            {
                //GET USER AND ROLE
                var role = GetUserRole(user.Id);
                AddSession(role, user);

                //Add COOKIE
                var userCookie = new HttpCookie("USER_REQUEST")
                {
                    ["REQUEST_USERNAME"] = request.UserName,
                    ["REQUEST_PASSWORD"] = request.Password,
                    Expires = DateTime.Now.AddDays(1d)
                };
                Response.Cookies.Add(userCookie);

                return RedirectToAction("Index", "Admin", new { Area = "Admin" });
            }

            ViewBag.Message = "Error while logging in!";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel request)
        {
            //CHECK MODEL_STATE
            if (!ModelState.IsValid)
            {
                return View(request);
            }
           
            //CHECK CAPTCHA
            var responseCaptcha = Request.Form["g-recaptcha-response"];
            var isCheckCaptcha = CaptchaHelper.CheckCaptcha(responseCaptcha);

            if (!isCheckCaptcha || IsNullOrEmpty(responseCaptcha)) 
            {
                ViewBag.Message = "Please Check Captcha";
                return View();
            }

            //CHECK PASSWORD
            if (request.Password != request.RePass)
            {
                ModelState.AddModelError("ErrorPass",
                    @"Your password does not match!!");
                return View();
            }

            //USER 
            var user = new AppUserIdentity
            {
                //Avatar = "https://i.ytimg.com/vi/hBm76i9jV9c/hqdefault.jpg",
                Email = request.Email,
                UserName = request.UserName,
            };

            var addUser = await UserManager.CreateAsync(user, request.Password);

            var userCreated = await UserManager.FindByNameAsync(user.UserName);
            var addRole = await UserManager.AddToRoleAsync(userCreated.Id, Roles.User.ToString());

            if (addUser.Succeeded && addRole.Succeeded)
            {
                ClearUser();
                return RedirectToAction("SignIn", "Account", new { Area = "" });
            }

            ViewBag.Message = "Get some error while register account";

            ModelState.AddModelError("Error", addUser.Errors.ToString());
            return View();
        }

        public ActionResult SignOut()
        {
            ClearUser();
            return RedirectToAction("SignIn");
        }

        private void ClearUser()
        {
            //get and clear userCookie
            var currentCookie = new HttpCookie("USER_REQUEST")
            {
                Expires = DateTime.Now.AddDays(-10)
            };

            //Request.Cookies.Clear();
            Response.Cookies.Add(currentCookie);

            //remove session
            ClearSession();
        }

        private void ClearSession()
        {
            Session.Clear();
            Session.Contents.RemoveAll();
        }

        private string GetUserRole(string userId)
        {
            var roleName = UserManager.GetRoles(userId).ToList().FirstOrDefault();
            var role = roleName?.ToLower();
            return role;
        }

        private void AddSession(string role, AppUserIdentity user)
        {
            //identity..
            switch (role)
            {
                case "user":
                    HttpContext.Session.Add("USER", user);
                    break;
                case "moderator":
                    HttpContext.Session.Add("MODERATOR", user);
                    break;
                default:
                    HttpContext.Session.Add("ADMIN", user);
                    break;
            }
        }

        public ActionResult GoogleLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GoogleLogin(ExternalLoginViewModel model)
        {
            //check user exist or not
            var userExists = await UserManager.FindByNameAsync(model.Name);

            if (userExists != null && string.Equals(userExists.External, model.External,
                StringComparison.CurrentCultureIgnoreCase))
            {
                var role = GetUserRole(userExists.Id);

                //add session
                ClearSession();
                AddSession(role, userExists);

                //add cookie
                var userCookie = new HttpCookie("USER_REQUEST")
                {
                    ["REQUEST_USERNAME"] = userExists.UserName,
                    ["REQUEST_EXTERNAL"] = userExists.External,
                    Expires = DateTime.Now.AddDays(1d)
                };
                Response.Cookies.Add(userCookie);

                return Json("signin");
            }

            /* If user does not exist but sign in with google account
             * - Register new account with external for user
             */
            var user = new AppUserIdentity
            {
                UserName = model.Name,
                Email = model.Email,
                Avatar = model.Image,
                External = model.External
            };

            //Auto generate password and create new user
            var password = PasswordHelper.GeneratePasswordHelper();
            var createUser = await UserManager.CreateAsync(user, password);

            //Sent mail for user to get their account infos
            var sendEmail = _emailService.SendMail(user.UserName, user.Email, password);
            if (!sendEmail.IsSuccess)
                return new JsonResult();

            /* Add role
             * - Then send user back to sign in page
             * - Let's user sign in again to access account
             */
            var userCreated = await UserManager.FindByNameAsync(user.UserName);
            var createRole = await UserManager.AddToRoleAsync(userCreated.Id, Roles.User.ToString());

            if (!createUser.Succeeded || !createRole.Succeeded)
                return new JsonResult();

            ClearUser();
            return Json("register");
        }
    }
}