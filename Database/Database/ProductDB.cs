using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public class ProductDB:IProductDB
    {
        private List<ProductPrice> Product
        {
            get;
            set;
        }

        public ProductDB()
        {
            Product = new List<ProductPrice>();
        }

        public ProductPrice AddNew(ProductPrice newProduct)
        {
            Product.Add(newProduct);
            return newProduct;
        }
        public ProductPrice Update(ProductPrice productToUpdate,string id)
        {
            foreach (ProductPrice product in GetAll())
            {
                if (product.ProductCode.Equals(id))
                {
                    product.ProductCode=productToUpdate.ProductCode;
                    product.FixedPrice = productToUpdate.FixedPrice;
                    break;
                }
            }
            return productToUpdate;
        }
        public void Delete(string code)
        {
          foreach (ProductPrice product in GetAll())
            {
                if (product.ProductCode.Equals(code))
                {
                    Product.Remove(product);

                    break;
                }
            }   
        }
        public List<ProductPrice> GetAll()
        {
            return Product;
        }
    }
}
