namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            AddColumn("dbo.Nutritions", "RecipeId", c => c.Int(nullable: false));
            AddColumn("dbo.Nutritions", "Name", c => c.String());
            AddColumn("dbo.Nutritions", "Value", c => c.Int(nullable: false));
            DropColumn("dbo.Recipes", "Nutritions");
            DropColumn("dbo.Nutritions", "UserId");
            DropColumn("dbo.Nutritions", "StepDescription");
            DropColumn("dbo.Nutritions", "StepImage");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Job");
            DropColumn("dbo.AspNetUsers", "Age");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Avatar = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Job", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.Nutritions", "StepImage", c => c.String(nullable: false));
            AddColumn("dbo.Nutritions", "StepDescription", c => c.String(nullable: false));
            AddColumn("dbo.Nutritions", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "Nutritions", c => c.String(nullable: false));
            DropColumn("dbo.Nutritions", "Value");
            DropColumn("dbo.Nutritions", "Name");
            DropColumn("dbo.Nutritions", "RecipeId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Recipes", "UserId");
            AddForeignKey("dbo.Recipes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
