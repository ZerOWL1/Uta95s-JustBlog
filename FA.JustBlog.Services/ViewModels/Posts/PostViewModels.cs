using FA.JustBlog.Core.Models.Enums;
using System;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class PostViewModels
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string PostContent { get; set; }
        public string UrlSlug { get; set; }
        public decimal Rate { get; set; }
        public int ViewCount { get; set; }
        public DateTime PostedOn { get; set; }
        public Status Status { get; set; }
    }
}