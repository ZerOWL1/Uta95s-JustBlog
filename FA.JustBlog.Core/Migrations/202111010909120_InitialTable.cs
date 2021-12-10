namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 1024),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        ShortDescription = c.String(name: "Short Description", nullable: false, maxLength: 750),
                        PostContent = c.String(name: "Post Content", nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Published = c.DateTime(),
                        PostedOn = c.DateTime(name: "Posted On", nullable: false, precision: 7, storeType: "datetime2"),
                        Modified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CategoryId = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        RateCount = c.Int(nullable: false),
                        TotalRate = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PostId = c.Int(nullable: false),
                        CommentHeader = c.String(),
                        CommentText = c.String(),
                        CommentTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 1024),
                        Count = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostTagMap",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTagMap", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTagMap", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostTagMap", new[] { "TagId" });
            DropIndex("dbo.PostTagMap", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("dbo.PostTagMap");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
