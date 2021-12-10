using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(t => t.UrlSlug)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute {IsUnique = true}));

            Property(t => t.Description)
                .HasMaxLength(1024);

            Property(t => t.Status)
                .IsRequired();
        }
    }
}