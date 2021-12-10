using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Core.Models.EntitiesBase
{
    public interface IBaseEntities
    {
        int Id { get; set; }
        Status Status { get; set; }
    }
}