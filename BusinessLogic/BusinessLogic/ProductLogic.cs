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
        /*private readonly IProductDB _productDB;

        public ProductLogic(IProductDB productDB)
        {
            _productDB = productDB;
        }*/

        private readonly IPricingBookDB _productTableDB;

        public ProductLogic(IPricingBookDB productTableDB)
        {
            _productTableDB = productTableDB;
        }

        public PricingBookDTO AddNewProduct(ProductPriceDTO newProduct, string id)
        {
            ProductPrice newProductPrice = new ProductPrice();
            newProductPrice.ProductCode = newProduct.ProductCode;
            newProductPrice.FixedPrice = newProduct.FixedPrice;
            PricingBook pricingBookInDB = _productTableDB.AddNewProduct(newProductPrice, id);

            return new PricingBookDTO()
            {
                Id = pricingBookInDB.Id,
                Name = pricingBookInDB.Name,
                Description = pricingBookInDB.Description,
                Status = pricingBookInDB.Status,
                ProductPrices = pricingBookInDB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    PromotionPrice = product.FixedPrice
                })

            };
        }

        public List<ProductPriceDTO> GetProducts(string id)
        {
            List<ProductPrice> allProducts = _productTableDB.GetProducts(id);
            List<ProductPriceDTO> products = new List<ProductPriceDTO>();

            foreach (ProductPrice pp in allProducts)
            {
                products.Add(
                    new ProductPriceDTO()
                    {
                        ProductCode = pp.ProductCode,
                        FixedPrice = pp.FixedPrice,
                        PromotionPrice = pp.FixedPrice
                    }

                );
            }

            return products;
        }


        public void UpdateProduct(List <ProductPriceDTO> productToUpdate,string id)
        {

            List<ProductPrice> productPriceupdated = new List<ProductPrice>();
            foreach(ProductPriceDTO product in productToUpdate){
                ProductPrice newproduct = new ProductPrice();
                newproduct.ProductCode = product.ProductCode;
                newproduct.FixedPrice = product.FixedPrice;
                productPriceupdated.Add(newproduct);
            }

            _productTableDB.UpdateProduct(productPriceupdated , id);

        }
       //ELIMINAR PRODUCTOS DE LISTA 
         public string DeleteProduct(string code)
         {
           string aux = "";
           List<ProductPriceDTO> priceslist = GetProducts(code);



            foreach (ProductPriceDTO pp in priceslist)
            {
                priceslist.Remove(pp);
                _productTableDB.DeleteProduct(code);  
                 aux = "PRICE LIST EXISTS AND PRODUCTS INSIDE WILL BE REMOVED ";
                return aux;
            }
            aux = "PRICE LIST DOES NOT EXIST ";
       
            return aux;
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
