using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class PostPendingViewModel
    {
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public string UserName { get; set; }
        public Status Status { get; set; }
    }
}