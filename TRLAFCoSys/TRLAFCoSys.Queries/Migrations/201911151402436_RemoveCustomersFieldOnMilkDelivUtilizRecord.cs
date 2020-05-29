namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomersFieldOnMilkDelivUtilizRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MilkDelivUtilizCustomers",
                c => new
                    {
                        MilkDelivUtilizCustomerID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        FullName = c.String(),
                        RawMilkSold = c.Double(nullable: false),
                        MilkDelivUtilizRecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MilkDelivUtilizCustomerID)
                .ForeignKey("dbo.MilkDelivUtilizRecords", t => t.MilkDelivUtilizRecordID, cascadeDelete: true)
                .Index(t => t.MilkDelivUtilizRecordID);
            
            DropColumn("dbo.MilkDelivUtilizRecords", "Customer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MilkDelivUtilizRecords", "Customer", c => c.String());
            DropForeignKey("dbo.MilkDelivUtilizCustomers", "MilkDelivUtilizRecordID", "dbo.MilkDelivUtilizRecords");
            DropIndex("dbo.MilkDelivUtilizCustomers", new[] { "MilkDelivUtilizRecordID" });
            DropTable("dbo.MilkDelivUtilizCustomers");
        }
    }
}
