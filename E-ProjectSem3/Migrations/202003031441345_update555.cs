namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update555 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contests", "SortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contests", "SortDescription");
        }
    }
}
