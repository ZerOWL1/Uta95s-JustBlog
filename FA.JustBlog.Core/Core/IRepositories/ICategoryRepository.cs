using System.Collections.Generic;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Core.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> GetCategoriesDeleted();
    }
}