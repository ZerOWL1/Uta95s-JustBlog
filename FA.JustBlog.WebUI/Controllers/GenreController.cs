using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Posts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Controllers
{
    public class GenreController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public GenreController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        // GET
        public ActionResult Index()
        {
            var categories = _categoryService.GetAllCategories().ToList();

            var postByCategory = new PostByCategoryViewModels
            {
                CategoryViewModels = categories
            };

            var postTitle = TempData["PostTitle"] as string;

            if (!string.IsNullOrEmpty(postTitle))
            {
                //Get posts by title
                var postFindByTitle = _postService.GetPostsByName(postTitle).ToList();
                postByCategory.PostViewModels = postFindByTitle;
            }
            else
            {
                //Get posts by category url
                var postFindByCategoryUrl = TempData["PostsByCategory"] as List<PostViewModels>;
                postByCategory.PostViewModels = postFindByCategoryUrl;
            }
            
            return View(postByCategory);
        }

        public ActionResult GetPostByCategory(string urlRequest)
        {
            var posts = _postService.GetPostsByCategory(urlRequest);
            TempData["PostsByCategory"] = posts;

            return RedirectToAction("Index");
        }

    }
}