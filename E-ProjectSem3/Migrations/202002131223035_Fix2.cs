namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
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
            
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Recipes", "UserId");
            AddForeignKey("dbo.Recipes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
