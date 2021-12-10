using AutoMapper;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.Enums;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Services.Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseResult Create(CreateCategoryViewModel request)
        {
            try
            {
                var category = new Category
                {
                    Name = request.Name,
                    UrlSlug = request.UrlSlug,
                    Description = request.Description
                };

                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();

                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult Edit(CreateCategoryViewModel request)
        {
            try
            {
                var category = new Category
                {
                    Id = request.Id,
                    Name = request.Name,
                    UrlSlug = request.UrlSlug,
                    Description = request.Description,
                    Status = request.Status
                };

                _unitOfWork.Categories.Update(category);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult Delete(string request)
        {
            try
            {
                var category = _unitOfWork.Categories.FindByUrlSlug(c => 
                    c.UrlSlug == request.Trim().ToLower());

                var posts = _unitOfWork.Posts.GetPostsByCategory(category.Name).ToList();

                foreach (var post in posts)
                {
                    post.CategoryId = null;
                    _unitOfWork.Posts.Update(post);
                }

                _unitOfWork.Categories.Delete(category.Id);

                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult Disable(string request)
        {
            try
            {
                var category = _unitOfWork.Categories.FindByUrlSlug(c => 
                    c.UrlSlug == request.Trim().ToLower());

                _unitOfWork.Categories.Delete(category.Id);

                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult RestoreDeletedCategory(string urlRequest)
        {
            try
            {
                var urlNormalize = urlRequest.Trim().ToLower();
                var category = _unitOfWork.Categories.FindByUrlSlug(c => c.UrlSlug == urlNormalize);

                category.Status = Status.IsPublished;
                _unitOfWork.Categories.Update(category);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public CreateCategoryViewModel GetCategory(string request)
        {
            try
            {
                var category = _unitOfWork.Categories.FindByUrlSlug(c => 
                    c.UrlSlug == request.Trim().ToLower());

                return Mapper.Map<CreateCategoryViewModel>(category);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return Mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public IEnumerable<CreateCategoryViewModel> GetCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return Mapper.Map<IEnumerable<CreateCategoryViewModel>>(categories);
        }

        public IEnumerable<CreateCategoryViewModel> GetCategoriesDeleted()
        {
            var categories = _unitOfWork.Categories.GetCategoriesDeleted();
            return Mapper.Map<IEnumerable<CreateCategoryViewModel>>(categories);
        }
    }
}