using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class SignInViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "This username field cannot empty!")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This password field cannot empty!")]
        public string Password { get; set; }
    }
}