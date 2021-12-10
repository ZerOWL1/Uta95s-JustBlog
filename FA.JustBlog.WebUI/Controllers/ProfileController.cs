using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Users;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly IUserService _userService;


        public ProfileController()
        {
        }

        public ProfileController(IUserService userService)
        {
            _userService = userService;
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

        // GET
        [HttpPost]
        public async Task<ActionResult> ChangeMail(ProfileViewModels request)
        {
            var email = request.ProfileViewModel.Email.Trim().ToLower();

            if (!(HttpContext.Session[0] is AppUserIdentity user))
                return RedirectToAction("SignIn", "Account");

            #region Update User Email

            var currentUser = await UserManager.FindByNameAsync(user.UserName);

            if (currentUser.Email == email)
            {
                return RedirectToAction("Profiles", "Post");
            }

            currentUser.Email = email;
            var userUpdateAsync = await UserManager.UpdateAsync(currentUser);

            if (!userUpdateAsync.Succeeded)
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);

            #endregion

            HttpContext.Session[0] = currentUser;
            return RedirectToAction("Profiles", "Post");
        }

        [HttpPost]
        public async Task<ActionResult> ChangeImage(ProfileViewModels request)
        {
            const string dir = @"~/wwwroot/assets/users/";

            if (!(HttpContext.Session[0] is AppUserIdentity user))
                return RedirectToAction("SignIn", "Account");

            var currentUser = await UserManager.FindByNameAsync(user.UserName);

            //Check user image folder exist or not
            var existsDir = Directory.Exists(Server.MapPath(dir + currentUser.UserName));
            if (!existsDir)
            {
                //Add new folder
                Directory.CreateDirectory(Server.MapPath(dir + currentUser.UserName));
            }

            //Get image file name
            var fileName = Path.GetFileNameWithoutExtension(request.ProfileViewModel.ImageFile.FileName);
            var extension = Path.GetExtension(request.ProfileViewModel.ImageFile.FileName);
            fileName = fileName + "_" + DateTime.Now.ToString("ddMMyyyy") + extension;

            //Update user avatar with new image directory
            currentUser.Avatar = dir + currentUser.UserName + "/" + fileName;

            fileName = Path.Combine(Server.MapPath(dir + currentUser.UserName), fileName);
            request.ProfileViewModel.ImageFile.SaveAs(fileName);

            //Save change update image and sent back response to view
            var userUpdateAsync = await UserManager.UpdateAsync(currentUser);

            if (!userUpdateAsync.Succeeded)
                return new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed);

            HttpContext.Session[0] = currentUser;
            return RedirectToAction("Profiles", "Post");
        }

        [HttpPost]
        public async Task<ActionResult> ChangePass(ProfileViewModels request)
        {
            if (!(HttpContext.Session[0] is AppUserIdentity user))
                return RedirectToAction("SignOut", "Account");

            //Check Models State
            if (!ModelState.IsValid)
            {
                TempData["ProfileFail"] = "Model State Invalid! - Change Pass State";
                return RedirectToAction("Profiles", "Post");
            }

            //Check enter true current password or not?
            var isSignIn = await SignInManager.PasswordSignInAsync(user.UserName,
                request.PasswordViewModel.OldPassword, true, false);

            if (isSignIn != SignInStatus.Success)
            {
                TempData["ProfileFail"] = "Old Password Not Match";
                return RedirectToAction("Profiles", "Post");
            }

            if (request.PasswordViewModel.NewPassword != request.PasswordViewModel.RePassword)
            {
                TempData["ProfileFail"] = "Re-Password does not similar";
                return RedirectToAction("Profiles", "Post");
            }

            /*
             * Get user and reset password - return to sign in.
             * Let's user sign in again
             */
            var currentUser = await UserManager.FindByNameAsync(user.UserName);

            var token = await UserManager.GeneratePasswordResetTokenAsync(currentUser.Id);

            var result = await UserManager.ResetPasswordAsync(user.Id,
                token, request.PasswordViewModel.NewPassword);

            #region Bugs while try to add Cookie with new password

            ////Add to session
            //HttpContext.Session[0] = currentUser;

            ////Add to cookie
            //var loginCookie = Request.Cookies["USER_REQUEST"];
            //if (loginCookie == null) 
            //    return RedirectToAction("Profiles", "Post");


            //loginCookie.Values["REQUEST_PASSWORD"] = 
            //    request.PasswordViewModel.NewPassword;
            //loginCookie.Expires = DateTime.Now.AddDays(1d);
            //Response.Cookies.Add(loginCookie);

            //return RedirectToAction("Profiles", "Post");

            #endregion

            ClearCookies();

            return !result.Succeeded ? RedirectToAction("Profiles", "Post")
                : RedirectToAction("SignOut", "Account");

        }

        public async Task<ActionResult> DisableAccount(string userName)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(userName);
                user.Status = Status.IsUnPublished;
                await UserManager.UpdateAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            return RedirectToAction("SignOut", "Account");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddUserPost(ProfileViewModels request)
        {
            var countSession = HttpContext.Session.Count;

            if (countSession <= 0 || !(HttpContext.Session[countSession - 1] is AppUserIdentity session))
                return RedirectToAction("SignOut", "Account");

            if (!ModelState.IsValid)
            {
                TempData["ProfileFail"] = "Model State Invalid! - Add Posts State";
                return RedirectToAction("Profiles", "Post");
            }

            //Add user post and wait admin approves
            var result = _userService.CreateUserPost(session.UserName,
                request.CreatePostViewModel);

            if (result.IsSuccess)
            {
                TempData["ProfileSuccess"] = "Add Post Success!";
                return RedirectToAction("Profiles", "Post");
            }

            TempData["ProfileFail"] = $"{session.UserName} Add Post Fail!";
            ModelState.AddModelError("ErrorAddPost", result.ErrorMessage);

            return RedirectToAction("Profiles", "Post");
        }

        private void ClearCookies()
        {
            //get and clear userCookie
            var currentCookie = new HttpCookie("USER_REQUEST")
            {
                Expires = DateTime.Now.AddDays(-10)
            };

            //Request.Cookies.Clear();
            Response.Cookies.Add(currentCookie);

            //remove session
            ClearSessions();
        }

        private void ClearSessions()
        {
            Session.Clear();
            Session.Contents.RemoveAll();
        }
    }
}