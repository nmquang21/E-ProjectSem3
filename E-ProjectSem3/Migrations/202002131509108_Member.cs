namespace E_ProjectSem3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member : DbMigration
    {
        public override void Up()
        {
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
                        CreatedAt = c.String(),
                        UpdatedAt = c.String(),
                        DeletedAt = c.String(),
                        OrderDescription = c.String(unicode:true),
                        BankCode = c.String(),
                        vnp_TransactionNo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vpn_Message = c.String(),
                        vpn_TxnResponseCode = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Memberships", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Memberships", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Memberships", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Memberships", new[] { "MemberId" });
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.Memberships");
            DropTable("dbo.Members");
        }
    }
}
