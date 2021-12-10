using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Image file cannot empty")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}