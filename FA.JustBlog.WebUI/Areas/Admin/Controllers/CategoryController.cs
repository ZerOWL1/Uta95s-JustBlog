using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Categories;
using System.Linq;
using System.Web.Mvc;

namespace FA.JustBlog.WebUI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Create()
        {
            var listCategories = new CategoriesViewModel();

            var categories = _categoryService.GetCategories();

            listCategories.CreateCategoryViewModels = categories.ToList();

            return View(listCategories);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CategoriesViewModel request)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid model error";
                return RedirectToAction("Create");
            }
            var response = _categoryService.Create(request.CreateCategoryViewModel);
            if (response.IsSuccess)
            {
                TempData["Message"] = "Create Category Success";
                return RedirectToAction("Create");
            }

            ViewData["ErrorMess"] = "Get some errors while add post";
            ModelState.AddModelError("ErrorAddCategory", response.ErrorMessage);
            return RedirectToAction("Create");
        }

        public ActionResult Delete(string urlRequest)
        {
            if (string.IsNullOrEmpty(urlRequest)) return RedirectToAction("Create");

            var response = _categoryService.Delete(urlRequest);
            if (response.IsSuccess)
            {
                return RedirectToAction("Create");
            }
            ViewData["ErrorMess"] = "Error while delete this record";
            return RedirectToAction("Create");
        }

        public ActionResult Edit(string urlRequest)
        {
            if (string.IsNullOrEmpty(urlRequest)) return RedirectToAction("Create");

            var category = _categoryService.GetCategory(urlRequest);
            
            return View(category);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CreateCategoryViewModel request)
        {
            if (request == null) return RedirectToAction("Create");

            var category = _categoryService.Edit(request);

            TempData["Message"] = "Edit Category Success";
            return RedirectToAction("Create");
        }

        public ActionResult Disable(string urlRequest)
        {
            if (string.IsNullOrEmpty(urlRequest)) return RedirectToAction("Create");

            var response = _categoryService.Disable(urlRequest);
            if (response.IsSuccess)
            {
                return RedirectToAction("Create");
            }
            ViewData["ErrorMess"] = "Error while delete this record";
            return RedirectToAction("Create");
        }

        public ActionResult Deleted()
        {
            var categories = _categoryService.GetCategoriesDeleted();
            return View(categories);
        }

        public ActionResult Restore(string urlRequest)
        {
            var result = _categoryService.RestoreDeletedCategory(urlRequest);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = $"Restore Category with Url '{urlRequest}' Successful";
                return RedirectToAction("Deleted");
            }

            TempData["ErrorMessage"] = $"Error while restore Category with Url: '{urlRequest}'";
            return RedirectToAction("Deleted");
        }
    }
}