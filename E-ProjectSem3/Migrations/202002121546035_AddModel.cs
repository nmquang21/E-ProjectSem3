namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        ApproveId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        NutritionId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        FeaturedImage = c.String(),
                        Detail = c.String(nullable: false),
                        Difficulty = c.String(nullable: false),
                        PreparationMinute = c.Int(nullable: false),
                        CookingMinute = c.Int(nullable: false),
                        CookingTemp = c.Int(nullable: false),
                        Nutritions = c.String(nullable: false),
                        Video = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Nutritions", t => t.NutritionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.NutritionId);
            
            CreateTable(
                "dbo.ContestUsers",
                c => new
                    {
                        ContestUserId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        ContestId = c.Int(nullable: false),
                        PrizeId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContestUserId)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.ContestId);
            
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        ContestId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Prize = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContestId);
            
            CreateTable(
                "dbo.Nutritions",
                c => new
                    {
                        NutritionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StepDescription = c.String(nullable: false),
                        StepImage = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.NutritionId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Value = c.Double(nullable: false),
                        ExtraInfor = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
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
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ApproveId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        Amout = c.Double(nullable: false),
                        Note = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.IngredientId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Prizes",
                c => new
                    {
                        PrizeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Money = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.PrizeId);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        WishListId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.WishListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Steps", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "NutritionId", "dbo.Nutritions");
            DropForeignKey("dbo.ContestUsers", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ContestUsers", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.Recipes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Steps", new[] { "RecipeId" });
            DropIndex("dbo.ContestUsers", new[] { "ContestId" });
            DropIndex("dbo.ContestUsers", new[] { "RecipeId" });
            DropIndex("dbo.Recipes", new[] { "NutritionId" });
            DropIndex("dbo.Recipes", new[] { "CategoryId" });
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropTable("dbo.WishLists");
            DropTable("dbo.Prizes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Steps");
            DropTable("dbo.Nutritions");
            DropTable("dbo.Contests");
            DropTable("dbo.ContestUsers");
            DropTable("dbo.Recipes");
            DropTable("dbo.Categories");
        }
    }
}
