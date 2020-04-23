using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.Controllers.DTOModels;
using CRM_PricingBooks.Database.Models;
namespace CRM_PricingBooks.BusinessLogic
{
    public class ProductLogic:IProductLogic
    {
        private readonly IProductDB _productDB;

        public ProductLogic(IProductDB productDB)
        {
            _productDB = productDB;
        }
        public void AddNewProduct(ProductPriceDTO newProduct)
        {
            // Mappers
            ProductPrice productprice = new ProductPrice();
            productprice.ProductCode = newProduct.ProductCode;
            productprice.FixedPrice = newProduct.FixedPrice;

            // Logic calculation            

            // Add to DB
            _productDB.AddNew(productprice);
        }
        private double calculatediscount(String activeCampaign, Double price) //Calculating discounts
        {

            if (activeCampaign == "XMAS")
            {
                price = price - price * (0.05);

            }
            if (activeCampaign == "SUMMER")
            {
                price = price - price * (0.20);
            }
            if (activeCampaign == "BFRIDAY")
            {
                price = price - price * (0.25);
            }
            else
            {
                return price;
            }
            return price;

        }

        public void UpdateProduct(ProductPriceDTO productToUpdate,string id)
        {
            List<ProductPrice> allProducts = _productDB.GetAll();
            foreach (ProductPrice product in allProducts)
            {
                if (product.ProductCode.Equals(id))
                {
                    product.ProductCode=productToUpdate.ProductCode;
                    product.FixedPrice = productToUpdate.FixedPrice;
                    break;
                }
            }
        }
        public void DeleteProduct(string code)
        {
            List<ProductPrice> allProducts = _productDB.GetAll();
            //List<ProductPriceDTO> products = new List<ProductPriceDTO>();

            foreach (ProductPrice product in allProducts)
            {
                if (product.ProductCode.Equals(code))
                {
                    allProducts.Remove(product);
                    break;
                }
            }
        }
        public List<ProductPriceDTO> GetAll()
        {
            List<ProductPrice> allProducts = _productDB.GetAll();
            List<ProductPriceDTO> products= new List<ProductPriceDTO>();

            // Mappers
            foreach (ProductPrice product in allProducts)
            {
                products.Add(
                    new ProductPriceDTO()
                    {
                        ProductCode = product.ProductCode,
                        PromotionPrice = product.FixedPrice
                        //calculatediscount("XMAS", product.FixedPrice)//change this price if there is any active campaign

                    }
                );
            }

            return products;
        }

       
    }
}
