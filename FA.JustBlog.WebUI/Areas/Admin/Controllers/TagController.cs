using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Tags;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Areas.Admin.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET
        public ActionResult Create()
        {
            var tags = _tagService.GetLatestTag().ToList();
            return View(tags);
        }

        public ActionResult Edit(string urlRequest)
        {
            var tagByUrlSlug = _tagService.GetTagByUrlSlug(urlRequest);
            return View(tagByUrlSlug);
        }

        [HttpPost]
        public ActionResult Edit(EditTagViewModel urlRequest)
        {
            var result = _tagService.Edit(urlRequest);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = $"Edit Tag has '{urlRequest.UrlSlug}' Succeeded!";
                return RedirectToAction("Create");
            }

            TempData["ErrorMessage"] = $"Edit Tag has '{urlRequest.UrlSlug}' Failed!";
            return RedirectToAction("Create");
        }
    }
}