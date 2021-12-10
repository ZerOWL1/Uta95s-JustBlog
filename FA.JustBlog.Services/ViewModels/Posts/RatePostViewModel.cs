using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class RatePostViewModel
    {
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "This rate field must be input")]
        public int Rate { get; set; }
    }
}