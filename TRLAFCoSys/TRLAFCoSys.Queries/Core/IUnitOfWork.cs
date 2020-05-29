using TRLAFCoSys.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get;}
        IRoleRepository Roles { get;}
        IActualDailySaleOutletRepo ActualDailySaleOutlets { get; }
        IActualDailySaleProductRepo ActualDailySaleProducts { get; }
        IDailySaleRecordRepo DailySaleRecords { get; }
        IFarmerRepo Farmers { get; }
        
        IMilkClassRepo MilkClasses { get; }
        IMilkUtilizeProductRepo MilkUtilizeProducts { get; }
        IMilkUtilizeProductRecordRepo MilkUtilizeProductRecords { get; }
        IMilkUtilizeRecordRepo MilkUtilizeRecords { get; }
        IMilkCollectionRepo MilkCollections { get; }
        IMilkUtilizeCustomerRepo MilkUtilizeCustomers { get; }
        IPayrollRepo Payrolls { get; }
        IExpenseTypeRepo ExpenseTypes { get; }
        IExpenseRepo Expenses { get; }
        ISupplyInventoryRepo SupplyInventories { get; }
        ISupplyTypeRepo SupplyTypes { get; }
        ISupplyClass SupplyClasses { get; }
        IProductRepo Products { get; }
        IProductSaleRepo ProductSales { get; }
        /// <summary>
        /// Raw Materials
        /// </summary>
        IProductRawMaterialRepo ProductRawMaterials { get; }
        IProductionRepo Productions { get; }
        int Complete();
    }
}
