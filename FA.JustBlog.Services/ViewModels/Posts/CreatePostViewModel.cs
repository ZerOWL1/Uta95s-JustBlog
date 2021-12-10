using System.ComponentModel.DataAnnotations;
using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Services.ViewModels.Posts
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "This title field cannot be empty!")]
        public string Title { get; set; }

        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "This short description field cannot be empty!")]
        public string ShortDescription { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This description field cannot be empty!")]
        public string Description { get; set; }

        [Display(Name = "Post Content")]
        [Required(ErrorMessage = "This post content field cannot be empty!")]
        public string PostContent { get; set; }

        [Display(Name = "UrlSlug")]
        [Required(ErrorMessage = "This url slug field cannot be empty!")]
        public string UrlSlug { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This category field cannot be empty!")]
        public int? CategoryId { get; set; }

        [Display(Name = "Tags (Note: must separate by ' ; ')")]
        [Required(ErrorMessage = "This tags field cannot be empty!")]
        public string Tags { get; set; }
        public Status Status { get; set; }
        public string UserName { get; set; }
    }
}