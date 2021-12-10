using AutoMapper;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Comments;
using System;
using System.Collections.Generic;

namespace FA.JustBlog.Services.Services.Implement
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CommentViewModel> GetCommentByPostId(int postId)
        {
            var comments = _unitOfWork.Comments.GetCommentsForPost(postId);
            return Mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }
        public ResponseResult AddCommentToPostById(string userName, CommentViewModel request)
        {
            try
            {
                var comment = new Comment
                {
                    Name = userName,
                    PostId = request.PostId,
                    CommentText = request.CommentText,
                };

                _unitOfWork.Comments.Add(comment);
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