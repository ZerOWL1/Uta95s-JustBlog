namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedTypeOfTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "CommentHeader", c => c.String(maxLength: 400));
            AlterColumn("dbo.Comments", "CommentText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentText", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Comments", "CommentHeader", c => c.String(nullable: false, maxLength: 400));
        }
    }
}
