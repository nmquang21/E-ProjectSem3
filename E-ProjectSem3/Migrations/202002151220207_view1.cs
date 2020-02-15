namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class view1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Image", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Image");
        }
    }
}