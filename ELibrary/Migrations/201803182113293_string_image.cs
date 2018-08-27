namespace ELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class string_image : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "image", c => c.Binary());
        }
    }
}
