using System.Collections.Generic;

namespace FA.JustBlog.Services.ViewModels.Categories
{
    public class CategoriesViewModel
    {
        public CreateCategoryViewModel CreateCategoryViewModel { get; set; }
        public ICollection<CreateCategoryViewModel> CreateCategoryViewModels { get; set; }
    }
}