using AutoMapper;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Services.Services.Implement
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ResponseResult Edit(EditTagViewModel request)
        {
            try
            {
                var tag = Mapper.Map<Tag>(request);
                _unitOfWork.Tags.Update(tag);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

        public EditTagViewModel GetTagByUrlSlug(string urlSlug)
        {
            var tags = _unitOfWork.Tags.GetTagByUrlSlugTag(urlSlug);
            return Mapper.Map<EditTagViewModel>(tags);
        }

        public string GetTagsByPostUrl(string postUrl)
        {
            var tags = _unitOfWork.Tags.GetTagByPostUrl(postUrl);

            var tagResult = tags.Aggregate(string.Empty, (current, tag) => current + (tag.Name + "; "));

            return tagResult.Trim();
        }

        public IEnumerable<EditTagViewModel> GetLatestTag()
        {
            var tags = _unitOfWork.Tags.GetAll();
            return Mapper.Map<IEnumerable<EditTagViewModel>>(tags);
        }
    }
}