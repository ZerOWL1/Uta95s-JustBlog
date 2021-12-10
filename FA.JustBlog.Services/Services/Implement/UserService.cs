using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Posts;
using System;
using System.Linq;
using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Services.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseResult AddUserImage(AppUserIdentity request)
        {
            try
            {

                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public ResponseResult CreateUserPost(string userName, CreatePostViewModel request)
        {
            try
            {
                var tagIds = _unitOfWork.Tags.AddTagByString(request.Tags);

                var postTags = tagIds.Select(tag => new PostTagMap {Tag = tag})
                    .ToList();

                var post = new Post
                {
                    Title = request.Title,
                    ShortDescription = request.ShortDescription,
                    PostContent = request.PostContent,
                    UrlSlug = request.UrlSlug,
                    CategoryId = request.CategoryId,
                    Description = request.Description,
                    UserName = userName,
                    PostTagMaps = postTags,
                    TotalRate = 1,
                    Status = Status.IsUnPublished
                };

                _unitOfWork.Posts.Add(post);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }
    }
}