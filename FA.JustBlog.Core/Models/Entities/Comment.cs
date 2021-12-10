using System;
using FA.JustBlog.Core.Models.EntitiesBase;

namespace FA.JustBlog.Core.Models.Entities
{
    public class Comment : BaseEntities
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string CommentHeader { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
    }
}