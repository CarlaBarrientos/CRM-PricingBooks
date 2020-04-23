using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public class ProductDatabase:IProductDB
    {
        private List<ProductPrice> Product
        {
            get;
            set;
        }

        public ProductDatabase()
        {
            Product = new List<ProductPrice>();
        }

        public void AddNew(ProductPrice newProduct)
        {
            Product.Add(newProduct);
        }
        public void Update(ProductPrice productToUpdate,string id)
        {
        }
        public void Delete(string code)
        {
        }
        public List<ProductPrice> GetAll()
        {
            return Product;
        }
    }
}
