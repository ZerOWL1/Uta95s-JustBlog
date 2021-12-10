using System;
using System.Collections.Generic;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Core.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        int CountPostsForCategory(string category);
        int CountPostsForTag(string tag);


        IEnumerable<Post> GetPublishedPosts();
        IEnumerable<Post> GetUnpublishedPosts();
        IEnumerable<Post> GetLatestPost();
        IEnumerable<Post> GetLatestPost(int size);
        IEnumerable<Post> GetLatestPost(int pageIndex, int size);
        IEnumerable<Post> GetPostsByMonth(DateTime monthYear);
        IEnumerable<Post> GetPostsByCategory(string category);
        IEnumerable<Post> GetPostsByUserName(string userName);
        IEnumerable<Post> GetPostsDoesNotApprove();
        IEnumerable<Post> GetPostsDeleted();
        IEnumerable<Post> GetPostsByTag(string tag);
        IEnumerable<Post> GetMostViewedPost(int size);
        IEnumerable<Post> GetHighestPosts(int size);


        PostTagMap GetPostByUrlSlug(string url);
    }
}