using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;

namespace TRLAFCoSys.Logic
{
    public static class Factories
    {
        public static ISupplyClassLogic CreateSupplyClass()
        {
            return new SupplyClassLogic();
        }
        public static ISupplyTypeLogic CreateSupplyType()
        {
            return new SupplyTypeLogic();
        }

        public static IProductLogic CreateProduct()
        {
            return new ProductLogic();
        }



        public static IProductionLogic CreateProduction()
        {
            return new ProductionLogic();
        }

        public static IProductSaleLogic CreateProductSale()
        {
            return new ProductSaleLogic();
        }

        public static IDailySaleRecordLogic CreateDailySale()
        {
            return new DailySaleRecordLogic();
        }

        public static IMilkUtilizeLogic CreateMilkUtilize()
        {
            return new MilkUtilizeLogic();
        }

        public static ISupplyInventoryLogic CreateSupplyInventory()
        {
            return new SupplyInventoryLogic();
        }

        public static IFarmerLogic CreateFarmers()
        {
            return new FarmerLogic();
        }

        public static IMilkCollectionLogic CreateMilkCollection()
        {
            return new MilkCollectionLogic();
        }

        public static IRawMilkProcessLogic CreateRawMilkProcess()
        {
            return new RawMilkProcessLogic();
        }

        public static IUserLogic CreateUser()
        {
            return new UserLogic();
        }



        
    }
}
