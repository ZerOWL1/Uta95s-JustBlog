namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedAvatarIdentityUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Avatar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Avatar", c => c.String());
        }
    }
}
