using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.DTOModels;
namespace CRM_PricingBooks.BusinessLogic
{
    public interface IProductLogic
    {

            void AddNewProduct(ProductPriceDTO newProduct);
            void UpdateProduct(ProductPriceDTO productToUpdate,string id);
            void DeleteProduct(string code);
            List<ProductPriceDTO> GetAll();

    }
}
