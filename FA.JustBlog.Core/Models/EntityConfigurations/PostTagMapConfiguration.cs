using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class PostTagMapConfiguration : EntityTypeConfiguration<PostTagMap>
    {
        public PostTagMapConfiguration()
        {
            ToTable("PostTagMap");

            HasKey(pt => new
            {
                pt.PostId,
                pt.TagId
            });

            HasRequired(pt => pt.Post)
                .WithMany(p => p.PostTagMaps)
                .HasForeignKey(pt => pt.PostId);

            HasRequired(pt => pt.Tag)
                .WithMany(t => t.PostTagMaps)
                .HasForeignKey(pt => pt.TagId);

            Ignore(pt => pt.Tags);
            Ignore(pt => pt.Posts);
        }
    }
}