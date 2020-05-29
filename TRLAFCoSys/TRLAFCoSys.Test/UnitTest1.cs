using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TRLAFCoSys.Queries;
using TRLAFCoSys.Queries.Persistence;
namespace TRLAFCoSys.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetSupplyTypeQuantityTest()
        {
            int supplyTypeID = 2;
            string date = "02/01/2020";

            using (var uow = new UnitOfWork(new DataContext()))
            {
                var products = uow.Productions.GetProducts(DateTime.Now, 1);
                
                
            }
        }
    }
}
