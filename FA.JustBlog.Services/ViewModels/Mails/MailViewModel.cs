using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Services.ViewModels.Mails
{
    public class MailViewModel
    {
        [Required(ErrorMessage = "Name field is required.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone field is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Message field is required.")]
        public string Message { get; set; }
    }
}