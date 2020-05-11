using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;
using Database.Database;

namespace CRM_PricingBooks.Database
{
    public interface IPricingBookDBManager : IDBManager
    {
        List<PricingBook> GetAll();
        PricingBook Update(PricingBook pricingBookToUpdate, string id);
        PricingBook AddNew(PricingBook newPricingBook);
        void Delete(string code);
        void Activate(string id);
        void DeActivate(string id);

        PricingBook AddNewProduct(List<ProductPrice> newProducts, string id);
        List<ProductPrice> GetProducts(string id);
        PricingBook UpdateProduct(List<ProductPrice> productToUpdate, string id);
        void DeleteProduct(string code);
        void DeleteProductCode(string code,string productcode);


    }
}