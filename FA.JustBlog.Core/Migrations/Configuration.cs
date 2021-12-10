using FA.JustBlog.Core.Context;

namespace FA.JustBlog.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FA.JustBlog.Core.Context.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FA.JustBlog.Core.Context.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            new BlogInitializer().SeedData(context);
        }
    }
}
