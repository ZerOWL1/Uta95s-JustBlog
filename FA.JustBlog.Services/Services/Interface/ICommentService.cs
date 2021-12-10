using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Comments;
using System.Collections.Generic;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface ICommentService
    {
        IEnumerable<CommentViewModel> GetCommentByPostId(int postId);
        ResponseResult AddCommentToPostById(string userName, CommentViewModel request);
    }
}