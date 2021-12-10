using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Posts;
using System.Collections.Generic;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface IPostService
    {
        ResponseResult Create(CreatePostViewModel request);
        ResponseResult Approve(string url);
        ResponseResult Delete(string request);
        ResponseResult HardDelete(string request);
        ResponseResult Edit(CreatePostViewModel request);
        ResponseResult CountAverageRateOfPost(string urlSlug, int ratePost);


        IEnumerable<PostViewModels> GetLatestPosts(int pageIndex, int size);
        IEnumerable<PostViewModels> GetPostsByTag(string tagName);
        IEnumerable<PostViewModels> GetPostsByCategory(string categoryName);
        IEnumerable<PostViewModels> GetPostsByName(string postTitle);
        IEnumerable<PostRelateViewModel> GetMostViewedPosts(int size);
        IEnumerable<PostRelateViewModel> GetHighestRatedPosts(int size);


        IEnumerable<PostPendingViewModel> GetPostsPending();
        IEnumerable<PostPendingViewModel> GetPostsDeleted();
        IEnumerable<CreatePostViewModel> GetLatestPosts();
        IEnumerable<CreatePostViewModel> GetAllPostsByUserName(string userName);

        Post FindPostById(int id);

        PostDetailViewModels GetPostByUrlSlug(string url);
        CreatePostViewModel GetCreatePostByUrl(string url);
        int CountPosts();
        int CountPendingPosts();
        int GetMaxPostsCount(int size);
        decimal GetTotalRateOfAllPost();
    }
}