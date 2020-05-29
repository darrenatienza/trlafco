namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductSales : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSales",
                c => new
                    {
                        ProductSaleID = c.Int(nullable: false, identity: true),
                        CreateTimeStamp = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                        CustomerName = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        isBuyOneTakeOne = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSaleID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSales", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductSales", new[] { "ProductID" });
            DropTable("dbo.ProductSales");
        }
    }
}
