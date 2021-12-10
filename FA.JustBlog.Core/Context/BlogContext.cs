using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.EntitiesBase;
using FA.JustBlog.Core.Models.EntityConfigurations;
using FA.JustBlog.Core.Models.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.JustBlog.Core.Context
{
    public class BlogContext : IdentityDbContext<AppUserIdentity>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTagMap> PostTagMaps { get; set; }

        //Identity and Authorization

        public BlogContext()
        :base("name=blogConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static BlogContext Create()
        {
            return new BlogContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new PostTagMapConfiguration());
        }

        public void BeforeComplete()
        {
            var entities = ChangeTracker.Entries();
            var dateNow = DateTime.Now;

            foreach (var entity in entities)
            {
                if (entity.Entity is IBaseEntities baseEntity)
                {
                    //switch (entity.State)
                    //{
                    //    case EntityState.Added:
                    //        baseEntity.Status = Status.IsPublished;
                    //        break;
                    //}

                    if (baseEntity is Post post)
                    {
                        switch (entity.State)
                        {
                            case EntityState.Added:
                                post.Published = dateNow;
                                post.PostedOn = dateNow;
                                post.Modified = dateNow;
                                post.TotalRate = 1;
                                //baseEntity.Status = Status.IsUnPublished;
                                break;
                            case EntityState.Modified:
                                post.Modified = dateNow;
                                break;
                        }
                    }

                    if (baseEntity is Comment comment)
                    {
                        switch (entity.State)
                        {
                            case EntityState.Added:
                                comment.CommentTime = dateNow;
                                var postComment = Posts.Find(comment.PostId);
                                if (postComment != null) postComment.ViewCount++;
                                break;
                            case EntityState.Modified:
                                comment.CommentTime = dateNow;
                                break;
                        }
                    }

                }
            }
        }
    }
}