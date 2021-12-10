namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameIdentityTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.UserClaims");
            DropTable("dbo.UserLogins");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserLogins", "IdentityUser_Id");
            CreateIndex("dbo.UserClaims", "IdentityUser_Id");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.UserRoles", "IdentityUser_Id");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.Roles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
