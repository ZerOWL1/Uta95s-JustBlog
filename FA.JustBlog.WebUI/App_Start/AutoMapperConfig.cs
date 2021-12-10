using AutoMapper;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Services.ViewModels.Categories;
using FA.JustBlog.Services.ViewModels.Comments;
using FA.JustBlog.Services.ViewModels.Posts;
using FA.JustBlog.Services.ViewModels.Tags;

namespace FA.JustBlog.WebUI
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            //posts
            CreateMap<CreatePostViewModel, Post>().ReverseMap();
            CreateMap<Post, PostViewModels>().ReverseMap();
            CreateMap<Post, PostRelateViewModel>().ReverseMap();
            CreateMap<PostTagMap, PostDetailViewModels>().ReverseMap();
            CreateMap<PostPendingViewModel, Post>().ReverseMap();

            //comments
            CreateMap<CommentViewModel, Comment>().ReverseMap();

            //categories
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CreateCategoryViewModel, Category>().ReverseMap();

            //tags
            CreateMap<EditTagViewModel, Tag>().ReverseMap();
            CreateMap<Tag, EditTagViewModel>().ReverseMap();
        }       
    }
}