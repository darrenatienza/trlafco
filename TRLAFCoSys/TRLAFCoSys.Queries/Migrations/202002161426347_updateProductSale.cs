namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductSales", "OutletSaleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductSales", "OutletSaleName");
        }
    }
}
