using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FA.JustBlog.Core.Models.Entities;

namespace FA.JustBlog.Core.Models.EntityConfigurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(400);

            Property(c => c.CommentHeader)
                .HasMaxLength(400);

            Property(c => c.CommentText)
                .IsRequired()
                .HasMaxLength(5000);

            Property(c => c.CommentTime)
                .HasColumnType("datetime2");
        }
    }
}