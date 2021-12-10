namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueToUrlSlugColInPostsTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Posts", "UrlSlug", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "UrlSlug" });
        }
    }
}
