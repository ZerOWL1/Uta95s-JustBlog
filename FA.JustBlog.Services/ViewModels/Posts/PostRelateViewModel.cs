using FA.JustBlog.Core.Models.Enums;
using System;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    /// <summary>
    /// This relate post used to load in index page
    /// <para>
    /// - Total Related:
    ///     1.  Highest Rated Post
    ///     2.  Most Viewed Post
    /// </para>
    /// </summary>
    public class PostRelateViewModel
    {
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public decimal Rate { get; set; }
        public int ViewCount { get; set; }
        public DateTime PostedOn { get; set; }
        public Status Status { get; set; }
    }
}