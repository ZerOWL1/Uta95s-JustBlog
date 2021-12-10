using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Image { get; set; }
        public string External { get; set; }
    }
}