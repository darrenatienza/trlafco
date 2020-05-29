namespace TRLAFCoSys.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_milkCollections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses");
            DropIndex("dbo.MilkCollections", new[] { "MilkClassID" });
            AddColumn("dbo.MilkCollections", "SupplyTypeID", c => c.Int());
            AlterColumn("dbo.MilkCollections", "MilkClassID", c => c.Int());
            CreateIndex("dbo.MilkCollections", "MilkClassID");
            CreateIndex("dbo.MilkCollections", "SupplyTypeID");
            AddForeignKey("dbo.MilkCollections", "SupplyTypeID", "dbo.SupplyTypes", "SupplyTypeID");
            AddForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses", "MilkClassID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses");
            DropForeignKey("dbo.MilkCollections", "SupplyTypeID", "dbo.SupplyTypes");
            DropIndex("dbo.MilkCollections", new[] { "SupplyTypeID" });
            DropIndex("dbo.MilkCollections", new[] { "MilkClassID" });
            AlterColumn("dbo.MilkCollections", "MilkClassID", c => c.Int(nullable: false));
            DropColumn("dbo.MilkCollections", "SupplyTypeID");
            CreateIndex("dbo.MilkCollections", "MilkClassID");
            AddForeignKey("dbo.MilkCollections", "MilkClassID", "dbo.MilkClasses", "MilkClassID", cascadeDelete: true);
        }
    }
}
