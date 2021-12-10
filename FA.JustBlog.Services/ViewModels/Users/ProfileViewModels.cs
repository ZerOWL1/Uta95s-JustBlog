using FA.JustBlog.Services.ViewModels.Posts;
using System.Collections.Generic;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class ProfileViewModels
    {
        public ProfileViewModel ProfileViewModel { get; set; }
        public PasswordViewModel PasswordViewModel { get; set; }
        public CreatePostViewModel CreatePostViewModel { get; set; }
        public ICollection<CreatePostViewModel> CreatePostViewModels { get; set; }
    }
}