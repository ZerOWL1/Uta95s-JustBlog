using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.ViewModels.Comments;
using System.Collections.Generic;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class PostDetailViewModels
    {
        public Post Post { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Tag Tag { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
        public ICollection<CommentViewModel> CommentViewModels { get; set; }
        public RatePostViewModel RatePostViewModel { get; set; }
    }
}