using System.Collections.Generic;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class PostsViewModel
    {
        public CreatePostViewModel CreatePostViewModel { get; set; }
        public ICollection<CreatePostViewModel> CreatePostViewModels { get; set; }
    }
}