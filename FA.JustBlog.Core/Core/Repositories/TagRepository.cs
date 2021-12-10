using System.Collections.Generic;
using System.Linq;
using FA.JustBlog.Common.CommonHelper.UrlHelper;
using FA.JustBlog.Core.Context;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Core.IRepositories;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Core.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(BlogContext context) : base(context)
        {
        }

        public Tag GetTagByUrlSlugTag(string urlSlug)
        {
            var urlNormalize = urlSlug.Trim().ToLower();

            return Context.Tags.SingleOrDefault(t => t.UrlSlug.Trim()
                                    .ToLower() == urlNormalize);
        }

        public IEnumerable<Tag> GetTagByPostUrl(string postUrl)
        {
            var urlNormalize = postUrl.Trim().ToLower();

            var tags = from t in Context.Tags
                join pt in Context.PostTagMaps on t.Id equals pt.TagId
                join p in Context.Posts on pt.PostId equals p.Id
                where p.UrlSlug.ToLower() == urlNormalize
                select t;

            return tags;
        }

        public IEnumerable<Tag> AddTagByString(string tags)
        {
            var tagNames = tags.Split(';');


            foreach (var tagName in tagNames)
            {
                if (!string.IsNullOrEmpty(tagName))
                {
                    var tagExist = Context.Tags
                        .SingleOrDefault(t => t.Name.Trim().ToLower() 
                                              == tagName.Trim().ToLower());

                    if (tagExist == null)
                    {
                        var tag = new Tag
                        {
                            Name = tagName.Trim(),
                            UrlSlug = UrlHelper.FriendlyUrl(tagName)
                        };
                        Context.Tags.Add(tag);
                    }
                }
            }

            Context.SaveChanges();

            return tagNames.Select(tagName => Context.Tags.FirstOrDefault
                    (
                        t => t.Name.Trim().ToLower() == tagName.Trim().ToLower()
                    ))
                    .Where(tagExist => tagExist != null)
                    .ToList();
        }
    }
}