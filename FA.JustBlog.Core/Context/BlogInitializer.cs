using FA.JustBlog.Core.Models.AppIdentities;
using FA.JustBlog.Core.Models.Entities;
using FA.JustBlog.Core.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FA.JustBlog.Core.Context
{
    public class BlogInitializer : CreateDatabaseIfNotExists<BlogContext>
    {
        public void SeedData(BlogContext context)
        {
            if (context.Categories.Any() && 
                context.Tags.Any() && context.Posts.Any() && 
                context.Roles.Any() && context.Users.Any() && 
                context.PostTagMaps.Any())
            {
                return;
            }

            #region Add Tags

            var tags = new Dictionary<int, Tag>
            {
                {1, new Tag{Name = "Anime", UrlSlug = "https://animetvn.com", Description = "Anime Tag", Count = 20} },
                {2, new Tag{Name = "Game", UrlSlug = "https://www.dota2.com/news", Description = "Dota 2 Game Tag", Count = 10} },
                {3, new Tag{Name = "News", UrlSlug = "https://vtv.vn", Description = "News Tag", Count = 0} },
                {4, new Tag{Name = "MSDN", UrlSlug = "https://docs.microsoft.com", Description = "MSDN Tag", Count = 5} },
                {5, new Tag{Name = "Github", UrlSlug = "https://github.com", Description = "Github Tag", Count = 9} },
            };

            foreach (var tag in tags.Values)
            {
                context.Tags.AddOrUpdate(t => t.Id, tag);
            }

            #endregion

            #region Add Categories

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Online",
                    Description = "Online Description",
                    UrlSlug = "https://www.dota2.com"
                },
                new Category
                {
                    Name = "Offline",
                    Description = "Offline Description",
                    UrlSlug = "https://www.aicarddass.com"
                },
                new Category
                {
                    Name = "Action",
                    Description = "Action Description",
                    UrlSlug = "https://www.ten-sura.com"
                },
                new Category
                {
                    Name = "Adventure",
                    Description = "Adventure Description",
                    UrlSlug = "https://www.nhk-character.com"
                },
                new Category
                {
                    Name = "Turn-Base",
                    Description = "Turn-Base Description",
                    UrlSlug = "https://ten-sura-m.bn-ent.net"
                }
            };

            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(c => c.Id, category);
            }

            #endregion

            #region Posts

            var dateNow = DateTime.Now;

            var posts = new List<Post>
            {
                new Post
                {
                    Title = "This Fall Marci Joins the Fight",
                    ShortDescription = "For fans of DOTA: Dragon's Blood, the next hero joining " +
                                       "the battle of the Ancients is already a familiar face",
                    PostContent = "Marci Joins the Fight",
                    UrlSlug = "https://www.dota2.com/newsentry/4245197977522229561",
                    PostedOn = dateNow,
                    Modified = dateNow,
                    Category = categories[0],
                    TotalRate = 1,
                },
                new Post
                {
                    Title = "Zenonzard AI Card GamePlay",
                    ShortDescription = "Zenonzard release now on GooglePlay and AppStore",
                    PostContent = "Zenonzard Release",
                    UrlSlug = "https://www.aicarddass.com/zenonzard",
                    PostedOn = dateNow,
                    Modified = dateNow,
                    Category = categories[1],
                    TotalRate = 1,
                },
                new Post
                {
                    Title = "Problems in - 26/10/2021",
                    ShortDescription = "Problems in 26/10/21 - VTV1",
                    PostContent = "Problems in 26/10/21 - VTV1",
                    UrlSlug = "https://vtv.vn/truyen-hinh-truc-tuyen/vtv1.htm",
                    PostedOn = dateNow,
                    Modified = dateNow,
                    Category = categories[2],
                    TotalRate = 1,
                },
            };

            foreach (var post in posts)
            {
                context.Posts.AddOrUpdate(p => p.Id, post);
            }

            #endregion

            #region Add PostTagMap

            var postTagMaps = new List<PostTagMap>
            {
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 2
                },
                new PostTagMap
                {
                    PostId = 2,
                    TagId = 2
                },
                new PostTagMap
                {
                    PostId = 3,
                    TagId = 3
                }
            };

            foreach (var postTagMap in postTagMaps)
            {
                context.PostTagMaps.Add(postTagMap);
            }

            #endregion

            #region Add Admin

            var userManager = new UserManager<AppUserIdentity>(
                new UserStore<AppUserIdentity>(context));

            var defaultAdmin = new AppUserIdentity
            {
                UserName = "admin",
                Email = "admin@gmail.com",
            };

            const string defaultPass = "123478@Kid";

            var createUserResult = userManager.Create(defaultAdmin, defaultPass);
            if (createUserResult.Succeeded)
            {
                var admin = userManager.FindByName(defaultAdmin.UserName);
                userManager.AddToRole(admin.Id, Roles.Admin.ToString());
            }

            #endregion

            #region Add Roles

            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    context.Roles.Add(new IdentityRole(role));
                }
            }

            #endregion

            context.SaveChanges();
        }
    }
}