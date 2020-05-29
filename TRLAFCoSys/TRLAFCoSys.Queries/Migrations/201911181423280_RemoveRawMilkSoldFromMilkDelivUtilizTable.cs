namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRawMilkSoldFromMilkDelivUtilizTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MilkDelivUtilizRecords", "BeginningVolume");
            DropColumn("dbo.MilkDelivUtilizRecords", "RawMilkSold");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MilkDelivUtilizRecords", "RawMilkSold", c => c.Double(nullable: false));
            AddColumn("dbo.MilkDelivUtilizRecords", "BeginningVolume", c => c.Double(nullable: false));
        }
    }
}
