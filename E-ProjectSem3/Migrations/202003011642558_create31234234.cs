namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create31234234 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Image = c.String(),
                        Icon = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApproveId = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        FeaturedImage = c.String(),
                        Detail = c.String(),
                        Difficulty = c.String(),
                        PreparationMinute = c.Int(nullable: false),
                        CookingMinute = c.Int(nullable: false),
                        CookingTemp = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Video = c.String(),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Avatar = c.String(),
                        Biography = c.String(),
                        Gender = c.String(),
                        Country = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Google = c.String(),
                        Website = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        RecipeId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false),
                        Rate = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        ApproveId = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContestRecipes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        Contest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.Contest_Id)
                .ForeignKey("dbo.Recipes", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Contest_Id);
            
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Money = c.Double(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        ContestRecipe_Id = c.Int(),
                        Contest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestRecipes", t => t.ContestRecipe_Id)
                .ForeignKey("dbo.Contests", t => t.Contest_Id)
                .Index(t => t.ContestRecipe_Id)
                .Index(t => t.Contest_Id);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.String(nullable: false),
                        Note = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Nutritions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        ImagePath = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        RecipeId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContestPrizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        ContestRecipe_Id = c.Int(nullable: false),
                        Prize_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestRecipes", t => t.ContestRecipe_Id, cascadeDelete: true)
                .ForeignKey("dbo.Prizes", t => t.Prize_Id, cascadeDelete: true)
                .Index(t => t.ContestRecipe_Id)
                .Index(t => t.Prize_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country_code = c.String(),
                        Country_name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberType = c.String(),
                        RoleName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiredMonths = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.OrderInfoes",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        OrderDescription = c.String(),
                        BankCode = c.String(),
                        vnp_TransactionNo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vpn_Message = c.String(),
                        vpn_TxnResponseCode = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Memberships", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Memberships", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestPrizes", "Prize_Id", "dbo.Prizes");
            DropForeignKey("dbo.ContestPrizes", "ContestRecipe_Id", "dbo.ContestRecipes");
            DropForeignKey("dbo.WishLists", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.WishLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Steps", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Nutritions", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.ContestRecipes", "Id", "dbo.Recipes");
            DropForeignKey("dbo.Prizes", "Contest_Id", "dbo.Contests");
            DropForeignKey("dbo.Prizes", "ContestRecipe_Id", "dbo.ContestRecipes");
            DropForeignKey("dbo.ContestRecipes", "Contest_Id", "dbo.Contests");
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recipes", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Recipes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderInfoes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Memberships", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Memberships", new[] { "MemberId" });
            DropIndex("dbo.ContestPrizes", new[] { "Prize_Id" });
            DropIndex("dbo.ContestPrizes", new[] { "ContestRecipe_Id" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropIndex("dbo.WishLists", new[] { "RecipeId" });
            DropIndex("dbo.Steps", new[] { "RecipeId" });
            DropIndex("dbo.Nutritions", new[] { "RecipeId" });
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            DropIndex("dbo.Prizes", new[] { "Contest_Id" });
            DropIndex("dbo.Prizes", new[] { "ContestRecipe_Id" });
            DropIndex("dbo.ContestRecipes", new[] { "Contest_Id" });
            DropIndex("dbo.ContestRecipes", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Recipes", new[] { "Category_Id" });
            DropIndex("dbo.Recipes", new[] { "ApplicationUser_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
            DropTable("dbo.Countries");
            DropTable("dbo.ContestPrizes");
            DropTable("dbo.WishLists");
            DropTable("dbo.Steps");
            DropTable("dbo.Nutritions");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Prizes");
            DropTable("dbo.Contests");
            DropTable("dbo.ContestRecipes");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Recipes");
            DropTable("dbo.Categories");
        }
    }
}
