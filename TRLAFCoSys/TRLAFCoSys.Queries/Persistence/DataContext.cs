using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TRLAFCoSys.Queries.Core.Domain;
using TRLAFCoSys.Queries.Migrations;
using TRLAFCoSys.Queries.EntityConfiguration;
namespace TRLAFCoSys.Queries.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext()
            //: base("DataContext")
            : base("ReleaseContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<ActualDailySaleOutlet> ActualDailySaleOutlets { get; set; }
        //public virtual DbSet<ActualDailySaleOutletReport> ActualDailySaleOutletReports { get; set; }
        //public virtual DbSet<ActualDailySaleProduct> ActualDailySaleProducts { get; set; }
        //public virtual DbSet<ActualDailySaleProductReport> ActualDailySaleProductReports { get; set; }
        public virtual DbSet<DailySaleRecord> DailySaleRecords { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<MilkClass> MilkClasses { get; set; }
        public virtual DbSet<MilkUtilizeProduct> MilkUtilizeProducts { get; set; }
        public virtual DbSet<MilkUtilizeProductRecord> MilkUtilizeProductRecords { get; set; }
        public virtual DbSet<MilkUtilizeCustomer> MilkUtilizeCustomers { get; set; }
        public virtual DbSet<MilkUtilizeRecord> MilkUtilizeRecords { get; set; }
        public virtual DbSet<MilkCollection> MilkCollections { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
        public virtual DbSet<SupplyInventory> SupplyInventories { get; set; }
        public virtual DbSet<SupplyType> SupplyTypes { get; set; }
        public virtual DbSet<SupplyClass> SupplyClasses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductRawMaterial> ProductRawMaterials { get; set; }
        public virtual DbSet<Production> Productions { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            
        }
    }
}
