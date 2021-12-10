using FA.JustBlog.Services.Results;
using FA.JustBlog.Services.ViewModels.Tags;
using System.Collections.Generic;

namespace FA.JustBlog.Services.Services.Interface
{
    public interface ITagService
    {
        ResponseResult Edit(EditTagViewModel request);

        EditTagViewModel GetTagByUrlSlug(string urlSlug);

        IEnumerable<EditTagViewModel> GetLatestTag();

        string GetTagsByPostUrl(string postUrl);
    }
}