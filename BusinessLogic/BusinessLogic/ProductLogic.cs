using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CRM_PricingBooks.Database;
using CRM_PricingBooks.DTOModels;
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
        public ProductPriceDTO AddNewProduct(ProductPriceDTO newProduct)
        {

            // Mappers
            ProductPrice productprice = new ProductPrice();
            productprice.ProductCode = Convert.ToString(new Random().Next(1,100));
            productprice.FixedPrice = newProduct.FixedPrice;


            // Add to DB
           ProductPrice productINDB= _productDB.AddNew(productprice);
             // Mappers => function: Productprices.FromEntityToDTO

            return new ProductPriceDTO(){
                ProductCode= productINDB.ProductCode,
                FixedPrice=productINDB.FixedPrice
            };
        }


        public ProductPriceDTO UpdateProduct(ProductPriceDTO productToUpdate,string id)
        {
            
            ProductPrice pprice = new ProductPrice();
           
            
            if(string.IsNullOrEmpty(productToUpdate.ProductCode))
            {
                pprice.ProductCode=null;
            }
            else
            {
                pprice.ProductCode=productToUpdate.ProductCode;
            }
          
            
            
            pprice.FixedPrice=productToUpdate.FixedPrice;
            
            ProductPrice proprice = _productDB.Update(pprice,id);

            return new ProductPriceDTO()
            {
            ProductCode =proprice.ProductCode,
            FixedPrice=proprice.FixedPrice
            
            };
        }
        public void DeleteProduct(string code)
        {
           

           _productDB.Delete(code);

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
                        FixedPrice=product.FixedPrice,
                        PromotionPrice = calculatediscount("XMAS", product.FixedPrice)

                    }
                );

            }

            return products;
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


    }
}
