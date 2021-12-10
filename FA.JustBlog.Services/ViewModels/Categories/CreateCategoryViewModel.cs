using FA.JustBlog.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Categories
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This name field should not empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This url slug field should not empty")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "This description field should not empty")]
        [MaxLength(255)]
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}