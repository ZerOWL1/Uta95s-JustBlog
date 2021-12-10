using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "This Email field cannot empty!")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "This Username field cannot empty!")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This Password field cannot empty!")]
        public string Password { get; set; }

        [Display(Name = "RePass")]
        [Required(ErrorMessage = "This RePasswords field cannot empty!")]
        public string RePass { get; set; }
    }
}