using System.Collections.Generic;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Core.IRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        void AddComment(int postId, string commentName, string commentEmail, 
            string commentTitle, string commentBody);

        IEnumerable<Comment> GetCommentsForPost(int postId);
        IEnumerable<Comment> GetCommentsForPost(Post post);
    }
}