using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Posts;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface IUserService
    {
        ResponseResult AddUserImage(AppUserIdentity request);
        ResponseResult CreateUserPost(string userName, CreatePostViewModel request);
    }
}