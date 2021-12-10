using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Old Passwords cannot empty!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Passwords cannot empty!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "RE-Passwords cannot empty!")]
        public string RePassword { get; set; }
    }
}