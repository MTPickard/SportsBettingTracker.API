namespace _01_SportsBetting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BettingInformationChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bet", "BetDescription", c => c.String(nullable: false));
            AddColumn("dbo.Bet", "BetOdds", c => c.Double(nullable: false));
            DropColumn("dbo.Bet", "BetParameters");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bet", "BetParameters", c => c.String());
            DropColumn("dbo.Bet", "BetOdds");
            DropColumn("dbo.Bet", "BetDescription");
        }
    }
}
