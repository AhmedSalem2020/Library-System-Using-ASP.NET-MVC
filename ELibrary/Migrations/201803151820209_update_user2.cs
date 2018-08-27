namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_user2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "fName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "lName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "lName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "fName", c => c.String(nullable: false));
        }
    }
}
