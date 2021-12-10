using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Services.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public Status Status { get; set; }
    }
}