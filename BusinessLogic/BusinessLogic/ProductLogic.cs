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
        private readonly IPricingBookDB _productTableDB;

        public ProductLogic(IPricingBookDB productTableDB)
        {
            _productTableDB = productTableDB;
        }

        public PricingBookDTO AddNewProduct(List<ProductPriceDTO> newProduct, string id)
        {
            List<ProductPrice> newProductPrice = new List<ProductPrice>();

            foreach(ProductPriceDTO productPrice in newProduct)
            {
                newProductPrice.Add(new ProductPrice()
                {
                    ProductCode = productPrice.ProductCode,
                    FixedPrice = productPrice.FixedPrice
                });
            }
            
            PricingBook pricingBookInDB = _productTableDB.AddNewProduct(newProductPrice, id);
            //Mapping PricingBook => PricingBookDTO
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


        public PricingBookDTO UpdateProduct(List <ProductPriceDTO> productToUpdate,string id)
        {

            List<ProductPrice> productPriceupdated = new List<ProductPrice>();
            foreach(ProductPriceDTO product in productToUpdate){
                ProductPrice newproduct = new ProductPrice();
                newproduct.ProductCode = product.ProductCode;
                newproduct.FixedPrice = product.FixedPrice;
                productPriceupdated.Add(newproduct);
            }

            PricingBook pricingBookInDB = _productTableDB.UpdateProduct(productPriceupdated , id);

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

         public string DeleteProduct(string code)
         {
           string aux = "";
           List<ProductPriceDTO> priceslist = GetProducts(code);

            foreach (ProductPriceDTO productprice in priceslist)
            {
                priceslist.Remove(productprice);
                _productTableDB.DeleteProduct(code);
                 aux = "PRICE LIST EXISTS AND PRODUCTS INSIDE WILL BE REMOVED ";
                return aux;
            }
            aux = "PRICE LIST DOES NOT EXIST ";
       
            return aux;
        }
        public string DeleteProductbyId(string code, string productcode){
        
         string aux = "";
           List<ProductPriceDTO> priceslist = GetProducts(code);
           

            foreach (ProductPriceDTO productpricedto in priceslist)
            {
                 if(productpricedto.ProductCode.Equals(productcode))
                  {
                    priceslist.Remove(productpricedto);
                    _productTableDB.DeleteProductCode(code,productcode);
                     aux = "PRICE LIST AND PRODUCTCODE EXIST, WILL BE REMOVED";
                    return aux;
                 }
               
            }
            aux = "PRICE LIST AND PRODUCTCODE DO NOT EXIST ";
       
            return aux;
        
        }
    }
}
