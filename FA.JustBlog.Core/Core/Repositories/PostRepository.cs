using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Core.IRepositories;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FA.JustBlog.Core.Core.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetPublishedPosts()
        {
            return Context.Posts.Where(p => p.Status == Status.IsPublished);
        }
        public IEnumerable<Post> GetUnpublishedPosts()
        {
            return Context.Posts.Where(p => p.Status == Status.IsUnPublished);
        }
        public IEnumerable<Post> GetLatestPost()
        {
            return Context.Posts.OrderByDescending(p => p.PostedOn);
        }
        public IEnumerable<Post> GetLatestPost(int size)
        {
            return Context.Posts.OrderByDescending(p => p.PostedOn)
                                .Take(size);
        }
        public IEnumerable<Post> GetLatestPost(int pageIndex, int size)
        {
            //1 - 0 - 5
            //2 - 5 - 5
            //3 - 10 - 5
            var current = (pageIndex - 1) * size;

            return Context.Posts.OrderByDescending(p => p.PostedOn)
                                .Where(p => p.Status == Status.IsPublished)
                                .Skip(current)
                                .Take(size);
        }
        public IEnumerable<Post> GetPostsByMonth(DateTime monthYear)
        {
            var inputMonth = monthYear.Month;
            return Context.Posts.Where(p => p.PostedOn.Month == inputMonth);
        }
        public IEnumerable<Post> GetPostsByCategory(string category)
        {
            var categoryNormalize = category.Trim().ToLower();

            return Context.Posts.Include(p => p.Category)
                                .Where(p => p.Category.Name.Trim()
                                    .ToLower() == categoryNormalize)
                                .OrderByDescending(p => p.PostedOn);
        }
        public IEnumerable<Post> GetPostsByUserName(string userName)
        {
            var userNameNormalize = userName.Trim().ToLower();

            return Context.Posts.Where(p => p.UserName.Trim().ToLower() == userNameNormalize);
        }
        public IEnumerable<Post> GetPostsDoesNotApprove()
        {
            return Context.Posts.Where(p => p.Status == Status.IsUnPublished)
                                .OrderByDescending(p => p.PostedOn);
        }
        public IEnumerable<Post> GetPostsDeleted()
        {
            return Context.Posts.Where(p => p.Status == Status.IsDeleted)
                                .OrderByDescending(p => p.PostedOn);
        }
        public IEnumerable<Post> GetPostsByTag(string urlTag)
        {
            var urlTagNormalize = urlTag.Trim().ToLower();

            var posts = from p in Context.Posts
                        join pt in Context.PostTagMaps on p.Id equals pt.PostId
                        join t in Context.Tags on pt.TagId equals t.Id
                        where t.UrlSlug.Trim().ToLower() == urlTagNormalize
                        select p;

            return posts;
        }
        public IEnumerable<Post> GetMostViewedPost(int size)
        {
            return Context.Posts.OrderByDescending(p => p.ViewCount)
                                .Take(size);
        }
        public IEnumerable<Post> GetHighestPosts(int size)
        {
            return Context.Posts.OrderByDescending(p => p.Rate)
                                .ThenBy(p => p.PostedOn)
                                .Take(size);
        }


        public int CountPostsForTag(string urlTag)
        {
            var urlTagNormalize = urlTag.Trim().ToLower();

            var posts = from p in Context.Posts
                join pt in Context.PostTagMaps on p.Id equals pt.PostId
                join t in Context.Tags on pt.TagId equals t.Id
                where t.UrlSlug.Trim().ToLower() == urlTagNormalize
                select p;

            var count = posts.Count();
            return count;
        }
        public int CountPostsForCategory(string category)
        {
            var categoryNormalize = category.Trim().ToLower();

            return Context.Posts.Include(p => p.Category)
                .Count(p => p.Category.Name.Trim()
                    .ToLower() == categoryNormalize);
        }


        public PostTagMap GetPostByUrlSlug(string url)
        {
            var urlNormalize = url.Trim().ToLower();

            var postTag = (from p in Context.Posts
                           join pt in Context.PostTagMaps on p.Id equals pt.PostId
                           join t in Context.Tags on pt.TagId equals t.Id
                           where p.UrlSlug.Trim().ToLower() == urlNormalize
                           select new
                           {
                               Post = p,
                               Tag = t
                           }).ToList();

            var tags = postTag.Select(t => t.Tag).ToList();

            var postTagMap = new PostTagMap
            {
                Post = postTag[0].Post,
                Tags = tags
            };

            return postTagMap;
        }
    }
}