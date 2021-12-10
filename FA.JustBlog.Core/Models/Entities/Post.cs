using System;
using System.Collections.Generic;
using FA.JustBlog.Core.Models.EntitiesBase;

namespace FA.JustBlog.Core.Models.Entities
{
    public class Post : BaseEntities
    {
        public Post()
        {
            PostTagMaps = new HashSet<PostTagMap>();
            Comments = new HashSet<Comment>();
        }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public DateTime? Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime Modified { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int ViewCount { get; set; }
        public decimal RateCount { get; set; }
        public int TotalRate { get; set; }
        public string UserName { get; set; }
        public decimal Rate
        {
            get => RateCount / TotalRate;
            set
            {
                if (value < 0) throw new ArgumentException(nameof(value));
            }
        }

        public virtual ICollection<PostTagMap> PostTagMaps { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}