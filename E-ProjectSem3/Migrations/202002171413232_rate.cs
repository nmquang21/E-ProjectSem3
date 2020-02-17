namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Rate", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Rate");
        }
    }
}
