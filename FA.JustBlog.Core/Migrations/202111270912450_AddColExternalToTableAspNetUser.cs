namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColExternalToTableAspNetUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "External", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "External");
        }
    }
}
