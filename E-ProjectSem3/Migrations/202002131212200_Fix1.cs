namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Nutritions", c => c.String(nullable: false));
            AddColumn("dbo.Nutritions", "RecipeId", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredients", "Note", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Difficulty", c => c.String(nullable: false));
            DropColumn("dbo.Nutritions", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nutritions", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Recipes", "Difficulty", c => c.Int(nullable: false));
            DropColumn("dbo.Ingredients", "Note");
            DropColumn("dbo.Nutritions", "RecipeId");
            DropColumn("dbo.Recipes", "Nutritions");
        }
    }
}
