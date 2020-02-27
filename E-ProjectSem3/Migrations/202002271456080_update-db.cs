namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContestUsers", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ContestUsers", "User_Id");
            AddForeignKey("dbo.ContestUsers", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContestUsers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContestUsers", new[] { "User_Id" });
            DropColumn("dbo.ContestUsers", "User_Id");
        }
    }
}
