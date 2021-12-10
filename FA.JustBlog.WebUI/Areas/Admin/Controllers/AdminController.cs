using System.Linq;
using System.Web;
using FA.JustBlog.WebUI.Filters;
using System.Web.Mvc;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Users;
using Microsoft.AspNet.Identity.Owin;

namespace FA.JustBlog.WebUI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostService _postService;
        private ApplicationUserManager _userManager;

        public AdminController(IPostService postService)
        {
            _postService = postService;
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        
        [CustomAuthorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            var countPost = _postService.CountPosts();
            var countUser = UserManager.Users.Count();
            var countPendingPost = _postService.CountPendingPosts();
            var countTotalRate = _postService.GetTotalRateOfAllPost();

            var statistics = new StatisticViewModel
            {
                TotalPost = countPost,
                TotalUser = countUser,
                PendingPost = countPendingPost,
                TotalRate = countTotalRate
            };

            return View(statistics);
        }

        [CustomAuthorize(Roles = "User")]
        public new ActionResult Profile()
        {
            return RedirectToAction("Profiles", "Post", new {Area = ""});
        }

        public ActionResult AccessDenied()
        {
            return new HttpStatusCodeResult(403);
        }
    }
}