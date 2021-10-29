namespace _01_SportsBetting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bet",
                c => new
                    {
                        BetId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        MatchUp = c.String(nullable: false),
                        BetParameters = c.String(),
                        BetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ToWin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsResolved = c.Boolean(nullable: false),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.BetId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: false) //
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: false) //
                .Index(t => t.MemberId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                        BookReference = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: false) //
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Credit = c.Double(nullable: false),
                        Debit = c.Double(nullable: false),
                        TransactionNote = c.String(),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: false) //
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: false) //
                .Index(t => t.MemberId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        BetId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        DidWin = c.Boolean(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Bet", t => t.BetId, cascadeDelete: false) //
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: false) //
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: false) //
                .Index(t => t.MemberId)
                .Index(t => t.BetId)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Bet", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Bet", "BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Transaction", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Result", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.Result", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Result", "BetId", "dbo.Bet");
            DropForeignKey("dbo.Transaction", "BookId", "dbo.Book");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Result", new[] { "TransactionId" });
            DropIndex("dbo.Result", new[] { "BetId" });
            DropIndex("dbo.Result", new[] { "MemberId" });
            DropIndex("dbo.Transaction", new[] { "BookId" });
            DropIndex("dbo.Transaction", new[] { "MemberId" });
            DropIndex("dbo.Book", new[] { "MemberId" });
            DropIndex("dbo.Bet", new[] { "BookId" });
            DropIndex("dbo.Bet", new[] { "MemberId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Result");
            DropTable("dbo.Member");
            DropTable("dbo.Transaction");
            DropTable("dbo.Book");
            DropTable("dbo.Bet");
        }
    }
}
