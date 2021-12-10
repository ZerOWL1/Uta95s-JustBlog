using System.Collections.Generic;
using FA.JustBlog.Core.Models.EntitiesBase;

namespace FA.JustBlog.Core.Models.Entities
{
    public class Category : BaseEntities
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}