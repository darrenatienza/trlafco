using TRLAFCoSys.Queries.Core;
using TRLAFCoSys.Queries.Core.IRepositories;
using TRLAFCoSys.Queries.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
            ActualDailySaleOutlets = new ActualDailySaleOutletRepo(_context);
            ActualDailySaleProducts = new ActualDailySaleProductRepo(_context);
            DailySaleRecords = new DailySaleRecordRepo(_context);
            Farmers = new FarmerRepo(_context);
            SupplyTypes = new SupplyTypeRepo(_context);
            MilkClasses = new MilkClassRepo(_context);
            MilkUtilizeProducts = new MilkUtilizeProductRepo(_context);
            MilkUtilizeRecords = new MilkUtilizeRecordRepo(_context);
            MilkUtilizeProductRecords = new MilkUtilizeProductRecordRepo(_context);
            MilkUtilizeCustomers = new MilkUtilizeCustomerRepo(_context);
            MilkCollections = new MilkCollectionRepo(_context);
            Payrolls = new PayrollRepo(_context);
            ExpenseTypes = new ExpenseTypeRepo(_context);
            Expenses = new ExpenseRepo(_context);
            SupplyInventories = new SupplyInventoryRepo(_context);
            SupplyClasses = new SupplyClassRepo(_context);
            Products = new ProductRepo(_context);
            ProductRawMaterials = new ProductRawMaterialRepo(_context);
            Productions = new ProductionRepo(_context);
            ProductSales = new ProductSaleRepo(_context);
        }

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IActualDailySaleOutletRepo ActualDailySaleOutlets
        { get; private set; }

        public IActualDailySaleProductRepo ActualDailySaleProducts
        { get; private set; }

        public IDailySaleRecordRepo DailySaleRecords
        { get; private set; }

        public IFarmerRepo Farmers
        { get; private set; }

        public ISupplyTypeRepo SupplyTypes
        { get; private set; }

        public IMilkClassRepo MilkClasses
        { get; private set; }

        public IMilkUtilizeProductRepo MilkUtilizeProducts
        { get; private set; }

        public IMilkUtilizeRecordRepo MilkUtilizeRecords
        { get; private set; }
        public IMilkCollectionRepo MilkCollections
        { get; private set; }

        public IPayrollRepo Payrolls
        { get; private set; }

        public IExpenseTypeRepo ExpenseTypes
        { get; private set; }

        public IExpenseRepo Expenses
        { get; private set; }

        public ISupplyInventoryRepo SupplyInventories
        { get; private set; }


        public IMilkUtilizeCustomerRepo MilkUtilizeCustomers
        {
            get;
            private set;
        }


        public IMilkUtilizeProductRecordRepo MilkUtilizeProductRecords
        {
            get;
            private set;
        }

        public ISupplyClass SupplyClasses
        {
            get;
            private set;
        }


        public IProductRepo Products
        {
            get;
            private set;
        }

        public IProductRawMaterialRepo ProductRawMaterials
        {
            get;
            private set;
        }


        public IProductionRepo Productions
        {
            get;
            private set;
        }


        public IProductSaleRepo ProductSales
        {
            get;
            private set;
        }
    }
}
