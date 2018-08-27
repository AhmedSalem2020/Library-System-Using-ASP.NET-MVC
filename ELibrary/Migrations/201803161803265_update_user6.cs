namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "image", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "birthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "JoinDate");
            DropColumn("dbo.AspNetUsers", "birthDate");
            DropColumn("dbo.AspNetUsers", "image");
        }
    }
}
