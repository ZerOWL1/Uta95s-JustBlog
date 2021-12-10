using System.Collections.Generic;
using System.Linq;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Core.IRepositories;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Core.Core.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesDeleted()
        {
            return Context.Categories.Where(c => c.Status == Status.IsDeleted);
        }
    }
}