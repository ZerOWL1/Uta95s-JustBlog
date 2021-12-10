using System.Collections.Generic;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class LoadPostViewModels
    {
        public IEnumerable<PostViewModels> Posts { get; set; }
        public IEnumerable<PostRelateViewModel> MostViewedPosts { get; set; }
        public IEnumerable<PostRelateViewModel> HighestRatedPosts { get; set; }
    }
}