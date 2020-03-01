namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedbs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contests", "Image", c => c.String());
            AlterColumn("dbo.Contests", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contests", "Name", c => c.String());
            AlterColumn("dbo.Contests", "Image", c => c.String(nullable: false));
        }
    }
}
