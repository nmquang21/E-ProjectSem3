namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Comments", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Comments", "DeletedAt", c => c.DateTime());
            CreateIndex("dbo.Comments", "RecipeId");
            AddForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            AlterColumn("dbo.Comments", "DeletedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
