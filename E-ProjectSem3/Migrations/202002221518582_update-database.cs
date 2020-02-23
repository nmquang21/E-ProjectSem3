namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderInfoes", "ApplicationUser_Id");
            AddForeignKey("dbo.OrderInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderInfoes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.OrderInfoes", "ApplicationUser_Id");
        }
    }
}
