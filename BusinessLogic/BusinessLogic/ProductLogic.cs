using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Exceptions;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.DTOModels;
using CRM_PricingBooks.Database.Models;
using BusinessLogic.DTOModels;

namespace CRM_PricingBooks.BusinessLogic
{
    public class ProductLogic:IProductLogic
    {
        private IPricingBookDBManager _productTableDB;

        public ProductLogic(IPricingBookDBManager productTableDB)
        {
            _productTableDB = productTableDB;
        }

        public PricingBookDTO AddNewProduct(List<ProductPriceDTO> newProduct, string id) //Create new product 
        {
            List<ProductPrice> newProductPrice = new List<ProductPrice>(); //DTO -> Database
            foreach(ProductPriceDTO productPrice in newProduct)
            {
                newProductPrice.Add(new ProductPrice()
                {
                    ProductCode = productPrice.ProductCode,
                    FixedPrice = productPrice.FixedPrice
                });
            }
            PricingBook pricingBookInDB = _productTableDB.AddNewProduct(newProductPrice, id);
            return DTOUtil.MapPricingBookDatabase_To_DTO(pricingBookInDB);
        }

        public List<ProductPriceDTO> GetProducts(string id)
        {
            List<ProductPrice> allProducts = _productTableDB.GetProducts(id);
            return DTOUtil.MapProductListDatabase_To_DTOList(allProducts);
        }


        public PricingBookDTO UpdateProduct(List <ProductPriceDTO> productToUpdate,string id)
        {

            try {
                List<ProductPrice> productPriceupdated = new List<ProductPrice>();
                if (productPriceupdated != null)
                {
                    foreach (ProductPriceDTO product in productToUpdate)
                    {
                        ProductPrice newproduct = new ProductPrice
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice
                        };
                        productPriceupdated.Add(newproduct);
                    }
                    PricingBook pricingBookInDB = _productTableDB.UpdateProduct(productPriceupdated, id);
                    return DTOUtil.MapPricingBookDatabase_To_DTO(pricingBookInDB);
                }
                else
                {
                    throw new BackingServiceException("PricingBook does not exist. ");
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("error while updating product" + ex.Message);
            }
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
