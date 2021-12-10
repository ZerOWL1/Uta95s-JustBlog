using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Posts;
using FA.JustBlog.Services.ViewModels.Users;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService
            , ICommentService commentService, ICategoryService categoryService)
        {
            _postService = postService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        [Route("index")]
        public ActionResult Index()
        {
            //Paging
            var pageIndex = 0;

            var currentIndex = TempData["PageIndex"] as int? ?? 0;
            pageIndex = currentIndex == 0 ? 1 : currentIndex;


            var posts = _postService.GetLatestPosts(pageIndex, 5).ToList();

            if (posts.Count <= 0)
            {
                pageIndex--;
                posts = _postService.GetLatestPosts(pageIndex, 5).ToList();
            }

            if (pageIndex < 0)
            {
                pageIndex = 1;
                posts = _postService.GetLatestPosts(pageIndex, 5).ToList();
            }

            TempData["PageMax"] = _postService.GetMaxPostsCount(5);
            TempData["PageIndex"] = pageIndex;

            //Relate Posts
            var mostViewedPosts = _postService.GetMostViewedPosts(5);
            var highestRatedPosts = _postService.GetHighestRatedPosts(5);

            var postViewModels = new LoadPostViewModels
            {
                Posts = posts,
                MostViewedPosts = mostViewedPosts,
                HighestRatedPosts = highestRatedPosts
            };

            return View(postViewModels);
        }

        /// <summary>
        /// Post Next Button
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Next(int pageIndex)
        {
            pageIndex++;
            TempData["PageIndex"] = pageIndex;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Post Previous Button
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Prev(int pageIndex)
        {
            pageIndex--;
            TempData["PageIndex"] = pageIndex;
            return RedirectToAction("Index");
        }

        public ActionResult Detail(string requestUrl)
        {
            if (string.IsNullOrEmpty(requestUrl))
                return RedirectToAction("Index");

            var post = _postService.GetPostByUrlSlug(requestUrl);

            var comments = _commentService.GetCommentByPostId(post.Post.Id).ToList();
            post.CommentViewModels = comments;

            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(PostDetailViewModels request)
        {
            //Get Current Post To Add Comments
            var post = _postService.FindPostById(request.CommentViewModel.PostId);

            var countSession = HttpContext.Session.Count;

            if (countSession <= 0 || !(HttpContext.Session[countSession - 1]
                is AppUserIdentity user))
                return RedirectToAction("Detail", "Post", new
                {
                    requestUrl = post.UrlSlug
                });

            //Add comments
            var result = _commentService.AddCommentToPostById(user.UserName,
                request.CommentViewModel);

            return result.IsSuccess ? Detail(post.UrlSlug) : new HttpStatusCodeResult(500);
        }

        public ActionResult PostTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return RedirectToAction("Index");

            var posts = _postService.GetPostsByTag(tagName).ToList();

            if (posts.Count <= 0) return RedirectToAction("Index");

            ViewBag.TagName = tagName;
            return View(posts);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Profiles()
        {
            //Get user session
            var countSession = HttpContext.Session.Count;

            if (countSession <= 0 || !(HttpContext.Session[countSession - 1]
                is AppUserIdentity session))
                return RedirectToAction("SignOut", "Account");


            var userProfile = new ProfileViewModel
            {
                Id = session.Id,
                UserName = session.UserName,
                Email = session.Email,
            };

            var categories = _categoryService.GetAllCategories().ToList();
            TempData["Categories"] = categories;

            var posts = _postService.GetAllPostsByUserName(session.UserName).ToList();

            var user = new ProfileViewModels
            {
                ProfileViewModel = userProfile,
                CreatePostViewModels = posts
            };

            return View(user);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string postTitle)
        {
            TempData["PostTitle"] = postTitle;
            return RedirectToAction("Index", "Genre");
        }

        [HttpPost]
        public ActionResult Rate(PostDetailViewModels request)
        {
            var result =
                _postService.CountAverageRateOfPost(request.RatePostViewModel.UrlSlug,
                    request.RatePostViewModel.Rate);

            //Check Rate Result
            if (result.IsSuccess)
            {
                TempData["ProfileSuccess"] = "Rate Post Success!";
                return RedirectToAction("Detail", "Post", new
                {
                    requestUrl = request.RatePostViewModel.UrlSlug
                });
            }

            TempData["ProfileFail"] = "Rate Post Fail!";
            return RedirectToAction("Detail", "Post", new
            {
                requestUrl = request.RatePostViewModel.UrlSlug
            });
        }
    }
}