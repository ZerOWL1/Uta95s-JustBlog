namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTagMapTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTagMap", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTagMap", "TagId", "dbo.Tags");
            DropIndex("dbo.PostTagMap", new[] { "PostId" });
            DropIndex("dbo.PostTagMap", new[] { "TagId" });
            CreateTable(
                "dbo.PostTagMaps",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            Sql("INSERT INTO dbo.PostTagMaps SELECT * FROM dbo.PostTagMap");
            DropTable("dbo.PostTagMap");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostTagMap",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId });
            Sql("INSERT INTO dbo.PostTagMap SELECT * FROM dbo.PostTagMaps");

            DropForeignKey("dbo.PostTagMaps", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTagMaps", "PostId", "dbo.Posts");
            DropIndex("dbo.PostTagMaps", new[] { "TagId" });
            DropIndex("dbo.PostTagMaps", new[] { "PostId" });
            DropTable("dbo.PostTagMaps");
            CreateIndex("dbo.PostTagMap", "TagId");
            CreateIndex("dbo.PostTagMap", "PostId");
            AddForeignKey("dbo.PostTagMap", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostTagMap", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
