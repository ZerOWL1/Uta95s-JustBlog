using AutoMapper;
using FA.JustBlog.Core.Core.UnitOfWork;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.Enums;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.Services.Interface;
using FA.JustBlog.Services.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Services.Services.Implement
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseResult Create(CreatePostViewModel request)
        {
            try
            {
                var tagIds = _unitOfWork.Tags.AddTagByString(request.Tags);

                var postTags = tagIds.Select(tag => new PostTagMap { Tag = tag })
                    .ToList();

                var post = new Post
                {
                    Title = request.Title,
                    ShortDescription = request.ShortDescription,
                    Description = request.Description,
                    PostContent = request.PostContent,
                    UrlSlug = request.UrlSlug,
                    CategoryId = request.CategoryId,
                    TotalRate = 1,
                    PostTagMaps = postTags,
                    Status = Status.IsPublished
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
        public ResponseResult Approve(string url)
        {
            try
            {
                var urlNormalize = url.Trim().ToLower();
                var post = _unitOfWork.Posts.FindByUrlSlug(p => p.UrlSlug.ToLower() == urlNormalize);

                post.Status = Status.IsPublished;
                _unitOfWork.Posts.Update(post);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }
        public ResponseResult Edit(CreatePostViewModel request)
        {
            try
            {
                var post = _unitOfWork.Posts.Find(request.Id);

                //GET TAG BY POST URL
                var tags = _unitOfWork.Tags.GetTagByPostUrl(request.UrlSlug);

                //DELETE TAGS OF POST - TAGS SAVED
                foreach (var tag in tags)
                {
                    var postTagMap = _unitOfWork.BlogContext.PostTagMaps
                        .SingleOrDefault(m => (m.PostId == post.Id) && (m.TagId == tag.Id));
                    if (postTagMap != null)
                    {
                        _unitOfWork.BlogContext.PostTagMaps.Remove(postTagMap);
                    }
                }

                //ADD NEW TAGS OF POST
                var tagIds = _unitOfWork.Tags.AddTagByString(request.Tags);

                var postTags = tagIds.Select(tag => new PostTagMap { Tag = tag })
                    .ToList();


                post.Title = request.Title;
                post.UrlSlug = request.UrlSlug;
                post.ShortDescription = request.ShortDescription;
                post.PostContent = request.PostContent;
                post.Description = request.Description;
                post.CategoryId = request.CategoryId;
                post.Status = request.Status;
                post.PostTagMaps = postTags;

                _unitOfWork.Posts.Update(post);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }
        public ResponseResult CountAverageRateOfPost(string urlSlug, int ratePost)
        {
            try
            {
                var urlNormalize = urlSlug.Trim().ToLower();
                var post = _unitOfWork.Posts.FindByUrlSlug(p => p.UrlSlug.ToLower() == urlNormalize);

                if (post == null)
                {
                    throw new Exception("Post return null exception!");
                }

                var rateCount = post.RateCount;
                var totalRate = post.TotalRate;

                //First time rate case
                if (rateCount == 0 && totalRate == 1)
                {
                    post.RateCount = ratePost;
                }
                //Otherwise case
                else
                {
                    post.TotalRate++;
                    post.RateCount = (rateCount + ratePost);
                }

                _unitOfWork.Posts.Update(post);
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
                var post = _unitOfWork.Posts.FindByUrlSlug(p =>
                    p.UrlSlug == request.Trim().ToLower());
                _unitOfWork.Posts.Delete(post.Id);

                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }
        public ResponseResult HardDelete(string request)
        {
            try
            {
                var post = _unitOfWork.Posts.FindByUrlSlug(p =>
                    p.UrlSlug == request.Trim().ToLower());
                _unitOfWork.Posts.Delete(post.Id, true);
                _unitOfWork.Complete();
                return new ResponseResult();
            }
            catch (Exception e)
            {
                return new ResponseResult(e.Message);
            }
        }

       
        public IEnumerable<PostPendingViewModel> GetPostsPending()
        {
            var posts = _unitOfWork.Posts.GetPostsDoesNotApprove();
            return Mapper.Map<IEnumerable<PostPendingViewModel>>(posts);
        }
        public IEnumerable<PostPendingViewModel> GetPostsDeleted()
        {
            var posts = _unitOfWork.Posts.GetPostsDeleted();
            return Mapper.Map<IEnumerable<PostPendingViewModel>>(posts);
        }
        public IEnumerable<CreatePostViewModel> GetAllPostsByUserName(string userName)
        {
            var posts = _unitOfWork.Posts.GetPostsByUserName(userName);

            return Mapper.Map<IEnumerable<CreatePostViewModel>>(posts);
        }
        public IEnumerable<CreatePostViewModel> GetLatestPosts()
        {
            var posts = _unitOfWork.Posts.GetLatestPost();
            return Mapper.Map<IEnumerable<CreatePostViewModel>>(posts);
        }


        public IEnumerable<PostRelateViewModel> GetHighestRatedPosts(int size)
        {
            var mostViewedPosts = _unitOfWork.Posts.GetHighestPosts(size);
            return Mapper.Map<IEnumerable<PostRelateViewModel>>(mostViewedPosts);
        }
        public IEnumerable<PostRelateViewModel> GetMostViewedPosts(int size)
        {
            var mostViewedPosts = _unitOfWork.Posts.GetMostViewedPost(size);
            return Mapper.Map<IEnumerable<PostRelateViewModel>>(mostViewedPosts);
        }


        public IEnumerable<PostViewModels> GetPostsByTag(string tagName)
        {
            var posts = _unitOfWork.Posts.GetPostsByTag(tagName);

            return Mapper.Map<IEnumerable<PostViewModels>>(posts);
        }
        public IEnumerable<PostViewModels> GetLatestPosts(int size)
        {
            var posts = _unitOfWork.Posts.GetLatestPost(size);
            return Mapper.Map<IEnumerable<PostViewModels>>(posts);
        }
        public IEnumerable<PostViewModels> GetLatestPosts(int pageIndex, int size)
        {
            var posts = _unitOfWork.Posts.GetLatestPost(pageIndex, size);
            return Mapper.Map<IEnumerable<PostViewModels>>(posts);
        }
        public IEnumerable<PostViewModels> GetPostsByCategory(string categoryName)
        {
            var posts = _unitOfWork.Posts.GetPostsByCategory(categoryName);
            return Mapper.Map<IEnumerable<PostViewModels>>(posts);
        }
        public IEnumerable<PostViewModels> GetPostsByName(string postTitle)
        {
            var postNormalize = postTitle.Trim().ToLower();
            var posts = _unitOfWork.Posts.Find(p => p.Title.Contains(postNormalize))
                                         .OrderByDescending(p => p.PostedOn);

            return Mapper.Map<IEnumerable<PostViewModels>>(posts);
        }


        public PostDetailViewModels GetPostByUrlSlug(string url)
        {
            var postTagMap = _unitOfWork.Posts.GetPostByUrlSlug(url);

            if (postTagMap == null) return null;

            var postView = _unitOfWork.Posts.Find(postTagMap.Post.Id);

            postView.ViewCount++;

            _unitOfWork.Posts.Update(postView);
            _unitOfWork.Complete();

            return Mapper.Map<PostDetailViewModels>(postTagMap);
        }
        public CreatePostViewModel GetCreatePostByUrl(string url)
        {
            var post = _unitOfWork.Posts.FindByUrlSlug(p => p.UrlSlug.ToLower() == url.Trim().ToLower());
            return Mapper.Map<CreatePostViewModel>(post);
        }


        public int CountPosts()
        {
            return _unitOfWork.Posts.GetAll().Count();
        }
        public int CountPendingPosts()
        {
            return _unitOfWork.Posts.GetPostsDoesNotApprove().Count();
        }
        public int GetMaxPostsCount(int size)
        {
            var postsCount = _unitOfWork.Posts.GetAll().Count();

            var count = postsCount / size;

            if (postsCount % size != 0)
            {
                count++;
            }

            return count;
        }


        public decimal GetTotalRateOfAllPost()
        {
            var sumOfTotalRate = _unitOfWork.BlogContext.Posts.Select(p => p.Rate).Sum();
            var countOfTotalRate = _unitOfWork.BlogContext.Posts.Select(p => p.Rate).Count();

            return sumOfTotalRate / countOfTotalRate;
        }


        public Post FindPostById(int id)
        {
            return _unitOfWork.Posts.Find(id);
        }
    }
}