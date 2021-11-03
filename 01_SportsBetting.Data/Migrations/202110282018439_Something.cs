namespace _01_SportsBetting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Something : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bet", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Book", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Transaction", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Member", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Result", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Result", "OwnerId");
            DropColumn("dbo.Member", "OwnerId");
            DropColumn("dbo.Transaction", "OwnerId");
            DropColumn("dbo.Book", "OwnerId");
            DropColumn("dbo.Bet", "OwnerId");
        }
    }
}
