namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBeginningVolumeOnMilkDelivUtilizRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MilkDelivUtilizRecords", "BeginningVolume", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MilkDelivUtilizRecords", "BeginningVolume");
        }
    }
}
