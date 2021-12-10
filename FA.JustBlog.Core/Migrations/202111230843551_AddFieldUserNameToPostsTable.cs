namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldUserNameToPostsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UserName");
        }
    }
}
