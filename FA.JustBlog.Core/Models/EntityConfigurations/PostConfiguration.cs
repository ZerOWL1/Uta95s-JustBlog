using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(255);

            Property(p => p.ShortDescription)
                .IsRequired()
                .HasMaxLength(750)
                .HasColumnName("Short Description");

            Property(p => p.PostContent)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("Post Content");

            Property(p => p.UrlSlug)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName, 
                    new IndexAnnotation(
                        new IndexAttribute { IsUnique = true }));

            Property(p => p.PostedOn)
                .HasColumnType("datetime2")
                .HasColumnName("Posted On");

            Property(p => p.Modified)
                .HasColumnType("datetime2");

            Property(p => p.UserName)
                .HasMaxLength(255);

            Property(p => p.Status)
                .IsRequired();

            //relationship
            HasOptional(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(true);

            HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PostId);
        }
    }
}