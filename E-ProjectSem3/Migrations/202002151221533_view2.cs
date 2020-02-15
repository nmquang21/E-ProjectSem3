namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class view2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Icon", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Icon");
        }
    }
}
