using System.Collections.Generic;

namespace FA.JustBlog.Core.Models.Entities
{
    public class PostTagMap
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public ICollection<Post> Posts { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}