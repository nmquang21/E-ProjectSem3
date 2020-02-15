namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Title", c => c.String());
            AlterColumn("dbo.Recipes", "Description", c => c.String());
            AlterColumn("dbo.Recipes", "Content", c => c.String());
            AlterColumn("dbo.Recipes", "Detail", c => c.String());
            AlterColumn("dbo.Recipes", "Difficulty", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Difficulty", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Detail", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Title", c => c.String(nullable: false));
        }
    }
}
