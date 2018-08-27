namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "phone", c => c.String());
        }
    }
}
