using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IProductSaleLogic : IBaseLogic<ProductSaleModel>
    {
        List<ProductSaleListModel> GetAll(string criteria);

        ProductSaleModel Get(int productSaleID);



        List<ProductSaleListModel> GetAllByMonth(DateTime date, string criteria);

        double ComputeTotal(double quantity, double unitPrice, double discount);
    }
}
