namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class view : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "ViewCount", c => c.Int(nullable: true));
            AlterColumn("dbo.Ingredients", "Amount", c => c.String(nullable: false));
            AlterColumn("dbo.Nutritions", "Value", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nutritions", "Value", c => c.Int(nullable: false));
            AlterColumn("dbo.Ingredients", "Amount", c => c.Double(nullable: false));
            DropColumn("dbo.Recipes", "ViewCount");
        }
    }
}
