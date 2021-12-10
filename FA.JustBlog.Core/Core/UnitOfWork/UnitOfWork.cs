using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.IRepositories;
using FA.JustBlog.Core.Core.Repositories;

namespace FA.JustBlog.Core.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository _categoryRepository;
        private ITagRepository _tagRepository;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        public BlogContext BlogContext { get; }


        public ITagRepository Tags => _tagRepository ?? 
                                      (_tagRepository = new TagRepository(BlogContext));
        public ICategoryRepository Categories => _categoryRepository ?? 
                                                 (_categoryRepository = new CategoryRepository(BlogContext));
        public IPostRepository Posts => _postRepository ?? 
                                        (_postRepository = new PostRepository(BlogContext));
        public ICommentRepository Comments => _commentRepository ?? 
                                              (_commentRepository = new CommentRepository(BlogContext));


        public UnitOfWork(BlogContext context)
        {
            BlogContext = context;
        }

        public void Dispose()
        {
            BlogContext.Dispose();
        }

        public void Complete()
        {
            BlogContext.BeforeComplete();

            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    BlogContext.SaveChanges();
                    break;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }

        public Task<int> CompleteAsync()
        {
            return BlogContext.SaveChangesAsync();
        }
    }
}