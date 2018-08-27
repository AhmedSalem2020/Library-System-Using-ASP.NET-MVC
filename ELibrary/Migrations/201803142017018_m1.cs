namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.authors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        fName = c.String(nullable: false),
                        lName = c.String(nullable: false),
                        image = c.String(),
                        birthDate = c.DateTime(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.books",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        copiesCount = c.Int(nullable: false),
                        availableCopies = c.Int(nullable: false),
                        title = c.String(nullable: false),
                        autherId = c.Int(nullable: false),
                        publisherId = c.Int(nullable: false),
                        categoryName = c.String(maxLength: 128),
                        cover = c.String(),
                        name = c.String(nullable: false),
                        source = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        joinDate = c.DateTime(nullable: false),
                        publishDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.authors", t => t.autherId, cascadeDelete: true)
                .ForeignKey("dbo.categories", t => t.categoryName)
                .ForeignKey("dbo.publishers", t => t.publisherId, cascadeDelete: true)
                .Index(t => t.autherId)
                .Index(t => t.publisherId)
                .Index(t => t.categoryName);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        name = c.String(nullable: false, maxLength: 128),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.name);
            
            CreateTable(
                "dbo.publishers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.userBooks",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        bookId = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        startDate = c.DateTime(nullable: false),
                        returnDate = c.DateTime(),
                        deliveredDate = c.DateTime(nullable: false),
                        isDelivered = c.Boolean(nullable: false),
                        employeeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.userId, t.bookId })
                .ForeignKey("dbo.books", t => t.bookId, cascadeDelete: true)
                .ForeignKey("dbo.employees", t => t.employeeId)
                .ForeignKey("dbo.members", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.bookId)
                .Index(t => t.employeeId);
            
            CreateTable(
                "dbo.employees",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.userId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.AspNetUsers",
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.members",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        isBlock = c.Boolean(nullable: false),
                        endDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.userBooks", "userId", "dbo.members");
            DropForeignKey("dbo.members", "id", "dbo.AspNetUsers");
            DropForeignKey("dbo.userBooks", "employeeId", "dbo.employees");
            DropForeignKey("dbo.employees", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.userBooks", "bookId", "dbo.books");
            DropForeignKey("dbo.books", "publisherId", "dbo.publishers");
            DropForeignKey("dbo.books", "categoryName", "dbo.categories");
            DropForeignKey("dbo.books", "autherId", "dbo.authors");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.members", new[] { "id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.employees", new[] { "userId" });
            DropIndex("dbo.userBooks", new[] { "employeeId" });
            DropIndex("dbo.userBooks", new[] { "bookId" });
            DropIndex("dbo.userBooks", new[] { "userId" });
            DropIndex("dbo.books", new[] { "categoryName" });
            DropIndex("dbo.books", new[] { "publisherId" });
            DropIndex("dbo.books", new[] { "autherId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.members");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.employees");
            DropTable("dbo.userBooks");
            DropTable("dbo.publishers");
            DropTable("dbo.categories");
            DropTable("dbo.books");
            DropTable("dbo.authors");
        }
    }
}
