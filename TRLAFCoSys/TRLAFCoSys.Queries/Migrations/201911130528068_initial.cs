namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActualDailySaleOutletReports",
                c => new
                    {
                        ActualDailySaleOutletReportID = c.Int(nullable: false, identity: true),
                        ActualDailySaleOutletID = c.Int(nullable: false),
                        ActualDailySaleReportID = c.Int(nullable: false),
                        Sales = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ActualDailySaleOutletReportID)
                .ForeignKey("dbo.ActualDailySaleOutlets", t => t.ActualDailySaleOutletID, cascadeDelete: true)
                .ForeignKey("dbo.ActualDailySaleReports", t => t.ActualDailySaleReportID, cascadeDelete: true)
                .Index(t => t.ActualDailySaleOutletID)
                .Index(t => t.ActualDailySaleReportID);
            
            CreateTable(
                "dbo.ActualDailySaleOutlets",
                c => new
                    {
                        ActualDailySaleOutletID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ActualDailySaleOutletID);
            
            CreateTable(
                "dbo.ActualDailySaleReports",
                c => new
                    {
                        ActualDailySaleReportID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        CustomerName = c.String(),
                        RawMilkQuantity = c.Double(nullable: false),
                        MultiplyBy = c.Double(nullable: false),
                        ProcessingSale = c.Double(nullable: false),
                        B1plus1 = c.Double(nullable: false),
                        DiscountSale = c.Double(nullable: false),
                        SaleOnAccount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ActualDailySaleReportID);
            
            CreateTable(
                "dbo.ActualDailySaleProductReports",
                c => new
                    {
                        ActualDailySaleProductReportID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDailySaleProductID = c.Int(nullable: false),
                        ActualDailySaleReportID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActualDailySaleProductReportID)
                .ForeignKey("dbo.ActualDailySaleProducts", t => t.ActualDailySaleProductID, cascadeDelete: true)
                .ForeignKey("dbo.ActualDailySaleReports", t => t.ActualDailySaleReportID, cascadeDelete: true)
                .Index(t => t.ActualDailySaleProductID)
                .Index(t => t.ActualDailySaleReportID);
            
            CreateTable(
                "dbo.ActualDailySaleProducts",
                c => new
                    {
                        ActualDailySaleProductID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ActualDailySaleProductID);
            
            CreateTable(
                "dbo.Farmers",
                c => new
                    {
                        FarmerID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.FarmerID);
            
            CreateTable(
                "dbo.MilkCollections",
                c => new
                    {
                        MilkCollectionID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        MilkClassID = c.Int(nullable: false),
                        FarmerID = c.Int(nullable: false),
                        Volume = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MilkCollectionID)
                .ForeignKey("dbo.Farmers", t => t.FarmerID, cascadeDelete: true)
                .ForeignKey("dbo.MilkClasses", t => t.MilkClassID, cascadeDelete: true)
                .Index(t => t.MilkClassID)
                .Index(t => t.FarmerID);
            
            CreateTable(
                "dbo.MilkClasses",
                c => new
                    {
                        MilkClassID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MilkClassID);
            
            CreateTable(
                "dbo.InventoryTypes",
                c => new
                    {
                        InventoryTypeID = c.Int(nullable: false, identity: true),
                        Description = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryTypeID);
            
            CreateTable(
                "dbo.MilkDelivUtilizProductRecords",
                c => new
                    {
                        MilkDelivUtilizProductRecordID = c.Int(nullable: false, identity: true),
                        MilkDelivUtilizProductID = c.Int(nullable: false),
                        MilkDelivUtilizRecordID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MilkDelivUtilizProductRecordID)
                .ForeignKey("dbo.MilkDelivUtilizProducts", t => t.MilkDelivUtilizProductID, cascadeDelete: true)
                .ForeignKey("dbo.MilkDelivUtilizRecords", t => t.MilkDelivUtilizRecordID, cascadeDelete: true)
                .Index(t => t.MilkDelivUtilizProductID)
                .Index(t => t.MilkDelivUtilizRecordID);
            
            CreateTable(
                "dbo.MilkDelivUtilizProducts",
                c => new
                    {
                        MilkDelivUtilizProductID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MilkDelivUtilizProductID);
            
            CreateTable(
                "dbo.MilkDelivUtilizRecords",
                c => new
                    {
                        MilkDelivUtilizRecordID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        Customer = c.String(),
                        RawMilkSold = c.Double(nullable: false),
                        WithdrawnByProcessor = c.String(),
                        RawMilkProcess = c.Double(nullable: false),
                        Spillage = c.Double(nullable: false),
                        Analysis = c.Double(nullable: false),
                        Demo = c.Int(nullable: false),
                        SpoilageQty = c.Double(nullable: false),
                        SpoilageValue = c.Double(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.MilkDelivUtilizRecordID);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        PayrollID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        FarmerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayrollID)
                .ForeignKey("dbo.Farmers", t => t.FarmerID, cascadeDelete: true)
                .Index(t => t.FarmerID);
            
            CreateTable(
                "dbo.RodraExpenses",
                c => new
                    {
                        RodraExpensesID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        Particulars = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RodraExpensesID);
            
            CreateTable(
                "dbo.RodraExpensesDescriptions",
                c => new
                    {
                        RodraExpensesDescriptionID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RodraExpensesDescriptionID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 250),
                        MiddleName = c.String(nullable: false, maxLength: 250),
                        LastName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.SuppliesInventories",
                c => new
                    {
                        SuppliesInventoryID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitCost = c.Double(nullable: false),
                        InventoryTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SuppliesInventoryID)
                .ForeignKey("dbo.InventoryTypes", t => t.InventoryTypeID, cascadeDelete: true)
                .Index(t => t.InventoryTypeID);
            
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        PermissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.PermissionID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PermissionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuppliesInventories", "InventoryTypeID", "dbo.InventoryTypes");
            DropForeignKey("dbo.UserPermissions", "PermissionID", "dbo.Roles");
            DropForeignKey("dbo.UserPermissions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Payrolls", "FarmerID", "dbo.Farmers");
            DropForeignKey("dbo.MilkDelivUtilizProductRecords", "MilkDelivUtilizRecordID", "dbo.MilkDelivUtilizRecords");
            DropForeignKey("dbo.MilkDelivUtilizProductRecords", "MilkDelivUtilizProductID", "dbo.MilkDelivUtilizProducts");
            DropForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses");
            DropForeignKey("dbo.MilkCollections", "FarmerID", "dbo.Farmers");
            DropForeignKey("dbo.ActualDailySaleProductReports", "ActualDailySaleReportID", "dbo.ActualDailySaleReports");
            DropForeignKey("dbo.ActualDailySaleProductReports", "ActualDailySaleProductID", "dbo.ActualDailySaleProducts");
            DropForeignKey("dbo.ActualDailySaleOutletReports", "ActualDailySaleReportID", "dbo.ActualDailySaleReports");
            DropForeignKey("dbo.ActualDailySaleOutletReports", "ActualDailySaleOutletID", "dbo.ActualDailySaleOutlets");
            DropIndex("dbo.UserPermissions", new[] { "PermissionID" });
            DropIndex("dbo.UserPermissions", new[] { "UserID" });
            DropIndex("dbo.SuppliesInventories", new[] { "InventoryTypeID" });
            DropIndex("dbo.Payrolls", new[] { "FarmerID" });
            DropIndex("dbo.MilkDelivUtilizProductRecords", new[] { "MilkDelivUtilizRecordID" });
            DropIndex("dbo.MilkDelivUtilizProductRecords", new[] { "MilkDelivUtilizProductID" });
            DropIndex("dbo.MilkCollections", new[] { "FarmerID" });
            DropIndex("dbo.MilkCollections", new[] { "MilkClassID" });
            DropIndex("dbo.ActualDailySaleProductReports", new[] { "ActualDailySaleReportID" });
            DropIndex("dbo.ActualDailySaleProductReports", new[] { "ActualDailySaleProductID" });
            DropIndex("dbo.ActualDailySaleOutletReports", new[] { "ActualDailySaleReportID" });
            DropIndex("dbo.ActualDailySaleOutletReports", new[] { "ActualDailySaleOutletID" });
            DropTable("dbo.UserPermissions");
            DropTable("dbo.SuppliesInventories");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RodraExpensesDescriptions");
            DropTable("dbo.RodraExpenses");
            DropTable("dbo.Payrolls");
            DropTable("dbo.MilkDelivUtilizRecords");
            DropTable("dbo.MilkDelivUtilizProducts");
            DropTable("dbo.MilkDelivUtilizProductRecords");
            DropTable("dbo.InventoryTypes");
            DropTable("dbo.MilkClasses");
            DropTable("dbo.MilkCollections");
            DropTable("dbo.Farmers");
            DropTable("dbo.ActualDailySaleProducts");
            DropTable("dbo.ActualDailySaleProductReports");
            DropTable("dbo.ActualDailySaleReports");
            DropTable("dbo.ActualDailySaleOutlets");
            DropTable("dbo.ActualDailySaleOutletReports");
        }
    }
}
