using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;
namespace CRM_PricingBooks.Database
{
    public interface IProductDB
    {
        void AddNew(ProductPrice newProduct);
        void Update(ProductPrice productToUpdate,string id);
        void Delete(string code);
        List<ProductPrice> GetAll();
    }
}
