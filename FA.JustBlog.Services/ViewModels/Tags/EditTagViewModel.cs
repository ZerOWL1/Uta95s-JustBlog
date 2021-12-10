using FA.JustBlog.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Tags
{
    public class EditTagViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is empty!")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required(ErrorMessage = "UrlSlug field is empty!")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "Status field is empty!")]
        public Status Status { get; set; }
    }
}