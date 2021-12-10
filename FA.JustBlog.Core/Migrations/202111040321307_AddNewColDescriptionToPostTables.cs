namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColDescriptionToPostTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Description");
        }
    }
}
