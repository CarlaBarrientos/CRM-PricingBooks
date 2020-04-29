using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.DTOModels;
namespace CRM_PricingBooks.BusinessLogic
{
    public interface IProductLogic
    {
        List<ProductPriceDTO> GetProducts(string id);
        PricingBookDTO AddNewProduct(ProductPriceDTO newProduct, string id);


        void UpdateProduct(List <ProductPriceDTO> productToUpdate,string id);
        void DeleteProduct(string code);

    }
}
