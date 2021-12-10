namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthPostContentPostTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Post Content", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Post Content", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
