using System.Collections.Generic;
using FA.JustBlog.Core.Models.EntitiesBase;

namespace FA.JustBlog.Core.Models.Entities
{
    public class Tag : BaseEntities
    {
        public Tag()
        {
            PostTagMaps = new HashSet<PostTagMap>();
        }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public virtual ICollection<PostTagMap> PostTagMaps { get; set; }
    }
}