namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contests", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Contests", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contests", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Contests", "Image");
        }
    }
}
