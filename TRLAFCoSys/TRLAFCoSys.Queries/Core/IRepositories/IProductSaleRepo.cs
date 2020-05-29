using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.IRepositories
{
    public interface IProductSaleRepo : IRepository<ProductSale>
    {

        IEnumerable<ProductSale> GetAll(string criteria);



        IEnumerable<ProductSale> GetAllByMonth(DateTime date, string criteria);
        /// <summary>
        /// Get Records by specific date and specific outlet
        /// </summary>
        /// <param name="date">Date to search</param>
        /// <param name="outlet">Name of the Outlet</param>
        /// <returns></returns>
        IEnumerable<ProductSale> GetAllBy(DateTime date, string outlet, string excludeProduct);
        /// <summary>
        /// Get records by date and product Name only
        /// </summary>
        ///  /// <param name="date">Exact date to filter</param>
        /// <param name="productName">Name of the product</param>
        /// <returns></returns>
        IEnumerable<ProductSale> GetAllBy(DateTime date,string productName);

        IEnumerable<ProductSale> GetAll(int supplyTypeID, int month, int year);
    }
}
