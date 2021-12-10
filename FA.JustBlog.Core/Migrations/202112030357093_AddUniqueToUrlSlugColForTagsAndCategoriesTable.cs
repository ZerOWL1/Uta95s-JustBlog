namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueToUrlSlugColForTagsAndCategoriesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "UrlSlug", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Categories", "UrlSlug", unique: true);
            CreateIndex("dbo.Tags", "UrlSlug", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "UrlSlug" });
            DropIndex("dbo.Categories", new[] { "UrlSlug" });
            AlterColumn("dbo.Tags", "UrlSlug", c => c.String(maxLength: 255));
        }
    }
}
