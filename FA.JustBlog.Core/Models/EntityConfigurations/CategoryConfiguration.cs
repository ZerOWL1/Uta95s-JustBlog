using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.UrlSlug)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute {IsUnique = true}));

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1024);
            
            Property(c => c.Status)
                .IsRequired();
        }
    }
}