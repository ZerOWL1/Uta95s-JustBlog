using System.Collections.Generic;
using FA.JustBlog.Core.Core.Infrastructures;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Core.IRepositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Tag GetTagByUrlSlugTag(string urlSlug);

        IEnumerable<Tag> GetTagByPostUrl(string postUrl);
        IEnumerable<Tag> AddTagByString(string tags);
    }
}