namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRequiredUrlSlugAndDescInTagsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "UrlSlug", c => c.String(maxLength: 255));
            AlterColumn("dbo.Tags", "Description", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Description", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.Tags", "UrlSlug", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
