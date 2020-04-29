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
        PricingBookDTO AddNewProduct(List<ProductPriceDTO> newProduct, string id);
        PricingBookDTO UpdateProduct(List <ProductPriceDTO> productToUpdate,string id);
        string DeleteProduct(string code);

    }
}
