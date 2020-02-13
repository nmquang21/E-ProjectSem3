namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Nutritions", "RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nutritions", "RecipeId", c => c.Int(nullable: false));
        }
    }
}
