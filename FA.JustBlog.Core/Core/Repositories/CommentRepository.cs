using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Core.IRepositories;
using FA.JustBlog.Core.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FA.JustBlog.Core.Core.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {
        }

        public void AddComment(int postId, string commentName, string commentEmail
            , string commentTitle, string commentBody)
        {
            var comment = new Comment
            {
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody
            };

            Context.Comments.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsForPost(int postId)
        {
            return Context.Comments.Include(c => c.Post)
                                   .Where(c => c.PostId == postId);
        }

        public IEnumerable<Comment> GetCommentsForPost(Post post)
        {
            return Context.Comments.Include(c => c.Post)
                                   .Where(c => c.Post.Id == post.Id);
        }
    }
}