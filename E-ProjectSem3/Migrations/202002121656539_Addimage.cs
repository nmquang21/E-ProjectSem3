namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Steps", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Steps", "ImagePath");
        }
    }
}
