using System;
using System.Threading.Tasks;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.IRepositories;

namespace FA.JustBlog.Core.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        BlogContext BlogContext { get; }

        ITagRepository Tags { get; }
        ICategoryRepository Categories { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }

        void Complete();
        Task<int> CompleteAsync();
    }
}