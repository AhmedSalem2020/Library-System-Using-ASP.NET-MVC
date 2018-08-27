namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "image");
            DropColumn("dbo.AspNetUsers", "birthDate");
            DropColumn("dbo.AspNetUsers", "JoinDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "birthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "image", c => c.Binary());
        }
    }
}
