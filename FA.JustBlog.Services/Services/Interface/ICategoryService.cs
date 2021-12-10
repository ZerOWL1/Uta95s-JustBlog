using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Categories;
using System.Collections.Generic;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface ICategoryService
    {
        ResponseResult Create(CreateCategoryViewModel request);
        ResponseResult Edit(CreateCategoryViewModel request);
        ResponseResult Delete(string request);
        ResponseResult Disable(string request);
        ResponseResult RestoreDeletedCategory(string urlRequest);
        
        CreateCategoryViewModel GetCategory(string request);

        IEnumerable<CategoryViewModel> GetAllCategories();
        IEnumerable<CreateCategoryViewModel> GetCategories();
        IEnumerable<CreateCategoryViewModel> GetCategoriesDeleted();
    }
}