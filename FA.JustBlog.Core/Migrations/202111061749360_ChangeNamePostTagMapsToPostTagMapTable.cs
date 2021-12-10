namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNamePostTagMapsToPostTagMapTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PostTagMaps", newName: "PostTagMap");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PostTagMap", newName: "PostTagMaps");
        }
    }
}
