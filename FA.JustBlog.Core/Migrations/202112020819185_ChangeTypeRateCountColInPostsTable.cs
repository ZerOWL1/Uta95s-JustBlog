namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeRateCountColInPostsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "RateCount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "RateCount", c => c.Int(nullable: false));
        }
    }
}
