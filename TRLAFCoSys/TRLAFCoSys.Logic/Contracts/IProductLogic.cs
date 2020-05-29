using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRLAFCoSys.Logic.Models;

namespace TRLAFCoSys.Logic.Contracts
{
    public interface IProductLogic : IBaseLogic<ProductModel>
    {
        List<ProductListModel> GetAll(string criteria);

        List<ProductRawMaterialListModel> GetAllProductRawMaterials(int productID);

        ProductModel Get(int productID);

        void EditProductRawMaterial(int productRawMaterialID, AddEditProductRawMaterialModel model);

        void AddProductRawMaterial(AddEditProductRawMaterialModel model);

        ProductRawMaterialModel GetProductRawMaterial(int productRawMaterialID);

        void DeleteProductRawMaterial(int productRawMaterialID);
    }
}
