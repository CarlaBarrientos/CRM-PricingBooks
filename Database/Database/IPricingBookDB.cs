using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public interface IPricingBookDB
    {
        List<PricingBook> GetAll();
        PricingBook Update(PricingBook pbToUpdate, string id);
        PricingBook AddNew(PricingBook newPricingBook);
        void Delete(string code);
        void Activate(string id);
        void DeActivate(string id);

        PricingBook AddNewProduct(List<ProductPrice> newProduct, string id);
        List<ProductPrice> GetProducts(string id);
        PricingBook UpdateProduct(List<ProductPrice> ppToUpdate, string id);
        void DeleteProduct(string code);


    }
}