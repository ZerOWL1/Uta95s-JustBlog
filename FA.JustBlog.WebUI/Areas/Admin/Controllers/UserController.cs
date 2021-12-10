using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.Enums;
using FA.JustBlog.Services.ViewModels.Users;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserController(ApplicationUserManager userManager
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

        // GET
        public ActionResult Index()
        {
            //Get user and their roles
            var usersExisting = (from user in _unitOfWork.BlogContext.Users
                                 select new
                                 {
                                     Username = user.UserName,
                                     user.Email,
                                     RoleNames = (from userRole in user.Roles
                                                  join role in _unitOfWork.BlogContext.Roles
                                                      on userRole.RoleId equals role.Id
                                                  select role.Name).ToList(),
                                     user.Status
                                 }).ToList().Select(p => new UserRolesViewModel()

                                 {
                                     Username = p.Username,
                                     Email = p.Email,
                                     Role = string.Join(", ", p.RoleNames),
                                     Status = p.Status
                                 }).ToList();

            return View(usersExisting);
        }

        public async Task<ActionResult> Ban(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            user.Status = Status.IsUnPublished;
            await UserManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UnBan(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            user.Status = Status.IsPublished;
            await UserManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Upgrade(string username)
        {
            await ChangeUserRole(username, Roles.User, Roles.Moderator);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeUpgrade(string username)
        {
            await ChangeUserRole(username, Roles.Moderator, Roles.User);
            return RedirectToAction("Index");
        }

        private async Task ChangeUserRole(string username, Roles currentRole, Roles nextRoles)
        {
            var user = await UserManager.FindByNameAsync(username);
            var deleteRole = await UserManager.RemoveFromRoleAsync(user.Id, currentRole.ToString());
            if (!deleteRole.Succeeded) return;

            await UserManager.AddToRoleAsync(user.Id, nextRoles.ToString());
        }
    }
}