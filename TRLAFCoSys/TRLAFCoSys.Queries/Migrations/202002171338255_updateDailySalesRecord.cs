namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDailySalesRecord : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DailySaleRecords", "MilkUtilizeCustomerID", "dbo.MilkUtilizeCustomers");
            DropIndex("dbo.DailySaleRecords", new[] { "MilkUtilizeCustomerID" });
            DropColumn("dbo.DailySaleRecords", "MilkUtilizeCustomerID");
            DropColumn("dbo.DailySaleRecords", "SaleAmount");
            DropColumn("dbo.DailySaleRecords", "B1plus1");
            DropColumn("dbo.DailySaleRecords", "DiscountSale");
            DropColumn("dbo.DailySaleRecords", "EcoBag");
            DropColumn("dbo.DailySaleRecords", "OutletSale1");
            DropColumn("dbo.DailySaleRecords", "OutletSale2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailySaleRecords", "OutletSale2", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "OutletSale1", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "EcoBag", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "DiscountSale", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "B1plus1", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "SaleAmount", c => c.Double(nullable: false));
            AddColumn("dbo.DailySaleRecords", "MilkUtilizeCustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.DailySaleRecords", "MilkUtilizeCustomerID");
            AddForeignKey("dbo.DailySaleRecords", "MilkUtilizeCustomerID", "dbo.MilkUtilizeCustomers", "MilkUtilizeCustomerID", cascadeDelete: true);
        }
    }
}
