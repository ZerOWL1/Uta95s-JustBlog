using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Services.ViewModels.Users
{
    public class UserRolesViewModel
    {
        public string Username { get; set; }  
        public string Email { get; set; }  
        public string Role { get; set; }  
        public Status Status { get; set; }  
    }
}