using System.Collections.Generic;
using FA.JustBlog.Services.ViewModels.Categories;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class PostByCategoryViewModels
    {
        public ICollection<CategoryViewModel> CategoryViewModels { get; set; }
        public ICollection<PostViewModels> PostViewModels { get; set; }
    }
}