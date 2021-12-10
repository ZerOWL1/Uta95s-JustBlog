using System;

namespace FA.JustBlog.Services.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string Name { get; set; }
        public string CommentText { get; set; }
        public int PostId { get; set; }
    }
}