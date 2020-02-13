namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Recipes", "UpdatedAt");
            DropColumn("dbo.Recipes", "DeletedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Recipes", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Recipes", "CreatedAt", c => c.DateTime());
        }
    }
}
