namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country_code = c.String(),
                        Country_name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Biography", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: true));
            AddColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Facebook", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Twitter", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Google", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "Website", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Website");
            DropColumn("dbo.AspNetUsers", "Google");
            DropColumn("dbo.AspNetUsers", "Twitter");
            DropColumn("dbo.AspNetUsers", "Facebook");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Biography");
            DropTable("dbo.Countries");
        }
    }
}
