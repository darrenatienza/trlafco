namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailySaleRecords",
                c => new
                    {
                        DailySaleRecordID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        MilkUtilizeCustomerID = c.Int(nullable: false),
                        SaleAmount = c.Double(nullable: false),
                        ProcessingSale = c.Double(nullable: false),
                        B1plus1 = c.Double(nullable: false),
                        DiscountSale = c.Double(nullable: false),
                        EcoBag = c.Double(nullable: false),
                        OutletSale1 = c.Double(nullable: false),
                        OutletSale2 = c.Double(nullable: false),
                        SaleOnAccount = c.Double(nullable: false),
                        Debtor = c.String(),
                    })
                .PrimaryKey(t => t.DailySaleRecordID)
                .ForeignKey("dbo.MilkUtilizeCustomers", t => t.MilkUtilizeCustomerID, cascadeDelete: true)
                .Index(t => t.MilkUtilizeCustomerID);
            
            CreateTable(
                "dbo.MilkUtilizeCustomers",
                c => new
                    {
                        MilkUtilizeCustomerID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        FullName = c.String(),
                        RawMilkSold = c.Double(nullable: false),
                        MilkUtilizeRecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MilkUtilizeCustomerID)
                .ForeignKey("dbo.MilkUtilizeRecords", t => t.MilkUtilizeRecordID, cascadeDelete: true)
                .Index(t => t.MilkUtilizeRecordID);
            
            CreateTable(
                "dbo.MilkUtilizeRecords",
                c => new
                    {
                        MilkUtilizeRecordID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        WithdrawnByProcessor = c.String(),
                        RawMilkProcess = c.Double(nullable: false),
                        Spillage = c.Double(nullable: false),
                        Analysis = c.Double(nullable: false),
                        Demo = c.Int(nullable: false),
                        SpoilageQty = c.Double(nullable: false),
                        SpoilageValue = c.Double(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.MilkUtilizeRecordID);
            
            CreateTable(
                "dbo.MilkUtilizeProductRecords",
                c => new
                    {
                        MilkUtilizeProductRecordID = c.Int(nullable: false, identity: true),
                        MilkUtilizeProductID = c.Int(nullable: false),
                        MilkUtilizeRecordID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MilkUtilizeProductRecordID)
                .ForeignKey("dbo.MilkUtilizeProducts", t => t.MilkUtilizeProductID, cascadeDelete: true)
                .ForeignKey("dbo.MilkUtilizeRecords", t => t.MilkUtilizeRecordID, cascadeDelete: true)
                .Index(t => t.MilkUtilizeProductID)
                .Index(t => t.MilkUtilizeRecordID);
            
            CreateTable(
                "dbo.MilkUtilizeProducts",
                c => new
                    {
                        MilkUtilizeProductID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MilkUtilizeProductID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        ExpenseTypeID = c.Int(nullable: false),
                        Particulars = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseID)
                .ForeignKey("dbo.ExpenseTypes", t => t.ExpenseTypeID, cascadeDelete: true)
                .Index(t => t.ExpenseTypeID);
            
            CreateTable(
                "dbo.ExpenseTypes",
                c => new
                    {
                        ExpenseTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ExpenseTypeID);
            
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
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductRawMaterials",
                c => new
                    {
                        ProductRawMaterialID = c.Int(nullable: false, identity: true),
                        SupplyTypeID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductRawMaterialID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.SupplyTypes", t => t.SupplyTypeID, cascadeDelete: true)
                .Index(t => t.SupplyTypeID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.SupplyTypes",
                c => new
                    {
                        SupplyTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        SupplyClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplyTypeID)
                .ForeignKey("dbo.SupplyClasses", t => t.SupplyClassID, cascadeDelete: true)
                .Index(t => t.SupplyClassID);
            
            CreateTable(
                "dbo.SupplyClasses",
                c => new
                    {
                        SupplyClassID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SupplyClassID);
            
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
                "dbo.SupplyInventories",
                c => new
                    {
                        SupplyInventoryID = c.Int(nullable: false, identity: true),
                        CreateDateTime = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(nullable: false),
                        PurchaseQuantity = c.Double(nullable: false),
                        WithdrawQuantity = c.Double(nullable: false),
                        SupplyTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplyInventoryID)
                .ForeignKey("dbo.SupplyTypes", t => t.SupplyTypeID, cascadeDelete: true)
                .Index(t => t.SupplyTypeID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupplyInventories", "SupplyTypeID", "dbo.SupplyTypes");
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProductRawMaterials", "SupplyTypeID", "dbo.SupplyTypes");
            DropForeignKey("dbo.SupplyTypes", "SupplyClassID", "dbo.SupplyClasses");
            DropForeignKey("dbo.ProductRawMaterials", "ProductID", "dbo.Products");
            DropForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses");
            DropForeignKey("dbo.MilkCollections", "FarmerID", "dbo.Farmers");
            DropForeignKey("dbo.Expenses", "ExpenseTypeID", "dbo.ExpenseTypes");
            DropForeignKey("dbo.DailySaleRecords", "MilkUtilizeCustomerID", "dbo.MilkUtilizeCustomers");
            DropForeignKey("dbo.MilkUtilizeProductRecords", "MilkUtilizeRecordID", "dbo.MilkUtilizeRecords");
            DropForeignKey("dbo.MilkUtilizeProductRecords", "MilkUtilizeProductID", "dbo.MilkUtilizeProducts");
            DropForeignKey("dbo.MilkUtilizeCustomers", "MilkUtilizeRecordID", "dbo.MilkUtilizeRecords");
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropIndex("dbo.SupplyInventories", new[] { "SupplyTypeID" });
            DropIndex("dbo.SupplyTypes", new[] { "SupplyClassID" });
            DropIndex("dbo.ProductRawMaterials", new[] { "ProductID" });
            DropIndex("dbo.ProductRawMaterials", new[] { "SupplyTypeID" });
            DropIndex("dbo.MilkCollections", new[] { "FarmerID" });
            DropIndex("dbo.MilkCollections", new[] { "MilkClassID" });
            DropIndex("dbo.Expenses", new[] { "ExpenseTypeID" });
            DropIndex("dbo.MilkUtilizeProductRecords", new[] { "MilkUtilizeRecordID" });
            DropIndex("dbo.MilkUtilizeProductRecords", new[] { "MilkUtilizeProductID" });
            DropIndex("dbo.MilkUtilizeCustomers", new[] { "MilkUtilizeRecordID" });
            DropIndex("dbo.DailySaleRecords", new[] { "MilkUtilizeCustomerID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.SupplyInventories");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.SupplyClasses");
            DropTable("dbo.SupplyTypes");
            DropTable("dbo.ProductRawMaterials");
            DropTable("dbo.Products");
            DropTable("dbo.MilkClasses");
            DropTable("dbo.MilkCollections");
            DropTable("dbo.Farmers");
            DropTable("dbo.ExpenseTypes");
            DropTable("dbo.Expenses");
            DropTable("dbo.MilkUtilizeProducts");
            DropTable("dbo.MilkUtilizeProductRecords");
            DropTable("dbo.MilkUtilizeRecords");
            DropTable("dbo.MilkUtilizeCustomers");
            DropTable("dbo.DailySaleRecords");
        }
    }
}
