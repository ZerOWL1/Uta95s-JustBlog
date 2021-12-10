namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionalCateIdForPostsTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            AlterColumn("dbo.Posts", "CategoryId", c => c.Int());
            CreateIndex("dbo.Posts", "CategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            AlterColumn("dbo.Posts", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "CategoryId");
        }
    }
}
