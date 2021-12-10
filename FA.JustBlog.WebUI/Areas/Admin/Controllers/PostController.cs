using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Posts;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        // GET

        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public PostController(IPostService postService, ICategoryService categoryService
            , ITagService tagService)
        {
            _tagService = tagService;
            _postService = postService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var categories = _categoryService.GetAllCategories().ToList();
            TempData["Categories"] = categories;

            var posts = _postService.GetLatestPosts().ToList();

            var postsVm = new PostsViewModel
            {
                CreatePostViewModels = posts
            };

            return View(postsVm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PostsViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = _postService.Create(request.CreatePostViewModel);
            if (response.IsSuccess)
            {
                TempData["Message"] = "Create Post Success";
                return RedirectToAction("Create");
            }

            TempData["ErrorMess"] = "Get some errors while add post";
            ModelState.AddModelError("ErrorAddPost", response.ErrorMessage);
            return RedirectToAction("Create");
        }

        public ActionResult Delete(string urlRequest)
        {
            //Delete post and send to recycle deleted bin
            if (string.IsNullOrEmpty(urlRequest)) return RedirectToAction("Create");

            var response = _postService.Delete(urlRequest);
            if (response.IsSuccess)
            {
                TempData["Message"] = "Set Post to deleted Success!!";
                return RedirectToAction("Create");
            }
            TempData["ErrorMess"] = "Error while delete this record";
            return RedirectToAction("Create");
        }

        public ActionResult Edit(string urlRequest)
        {
            if (string.IsNullOrEmpty(urlRequest)) return RedirectToAction("Create");

            var post = _postService.GetCreatePostByUrl(urlRequest);

            var tags = _tagService.GetTagsByPostUrl(urlRequest);
            post.Tags = tags;

            var categories = _categoryService.GetAllCategories().ToList();
            TempData["Categories"] = categories;

            //return View(category);
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CreatePostViewModel request)
        {
            if (request == null) return RedirectToAction("Create");

            var post = _postService.Edit(request);
            if (post.IsSuccess)
            {
                TempData["Message"] = $"Edit Post '{request.UrlSlug}' Success";
                return RedirectToAction("Create");
            }

            TempData["ErrorMess"] = $"Edit Post '{request.UrlSlug}' Fail";
            return RedirectToAction("Create");
        }

        public ActionResult Pending()
        {
            var posts = _postService.GetPostsPending().ToList();
            return View(posts);
        }

        public ActionResult Deleted()
        {
            var posts = _postService.GetPostsDeleted().ToList();
            return View(posts);
        }

        public ActionResult Approve(string urlRequest, string actions, string redirect)
        {
            var result = _postService.Approve(urlRequest);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = $"Post Url '{urlRequest}' {actions}";
                return RedirectToAction($"{redirect}");
            }

            TempData["ErrorMessage"] = $"Post Url '{urlRequest}' {actions} Fail";
            return RedirectToAction($"{redirect}");
        }

        public ActionResult HardDelete(string urlRequest)
        {
            var result = _postService.HardDelete(urlRequest);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = $"Delete Post with url '{urlRequest}' success";
                return RedirectToAction("Deleted");
            }

            TempData["ErrorMessage"] = $"Delete Post with url '{urlRequest}' fail";
            return RedirectToAction("Deleted");
        }
    }
}