using FA.JustBlog.Core.Models.Enums;

namespace FA.JustBlog.Core.Models.EntitiesBase
{
    public class BaseEntities : IBaseEntities
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}