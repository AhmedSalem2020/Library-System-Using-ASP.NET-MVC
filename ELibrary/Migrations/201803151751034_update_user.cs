namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "fName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "lName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "image", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "birthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "address", c => c.String());
            AddColumn("dbo.AspNetUsers", "phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "firstLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "firstLogin");
            DropColumn("dbo.AspNetUsers", "isDeleted");
            DropColumn("dbo.AspNetUsers", "JoinDate");
            DropColumn("dbo.AspNetUsers", "phone");
            DropColumn("dbo.AspNetUsers", "address");
            DropColumn("dbo.AspNetUsers", "birthDate");
            DropColumn("dbo.AspNetUsers", "image");
            DropColumn("dbo.AspNetUsers", "lName");
            DropColumn("dbo.AspNetUsers", "fName");
        }
    }
}
