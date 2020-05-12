using CRM_PricingBooks.Database.Models;
using CRM_PricingBooks.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOModels
{
   public class DTOUtil
    {
        //Convert PricingBook LIST from Database to PricingBookDTO LIST
        public static List<PricingBookDTO> MapPricingBookListDatabase_To_DTOList(List<PricingBook> pricingBookList)
        {
            List<PricingBookDTO> pricingBookDTOList = new List<PricingBookDTO>();

            foreach (PricingBook pricingBook in pricingBookList)
            {
                pricingBookDTOList.Add
                (
                    new PricingBookDTO()
                    {
                        Name = pricingBook.Name,
                        Description = pricingBook.Description,
                        ProductPrices = pricingBook.ProductsList.ConvertAll(product => new ProductPriceDTO
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice,
                           
                        })
                    }
                );
            }

            return pricingBookDTOList;
        }

        //Convert PricingBook LIST from Database to PricingBookDTO LIST
        public static PricingBookDTO MapPricingBookDatabase_To_DTO(PricingBook pricingBook)
        {
            PricingBookDTO pricingBookDTO = new PricingBookDTO {
                   
                        Name = pricingBook.Name,
                        Description = pricingBook.Description,
                        ProductPrices = pricingBook.ProductsList.ConvertAll(product => new ProductPriceDTO
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice,

                        })
                };
            
            return pricingBookDTO;
        }
 //------------------------------------------------------------------------------------------------------

        //Convert Product Price LIST from Database to ProductPriceDTO LIST
        public static List<ProductPriceDTO> MapProductListDatabase_To_DTOList(List<ProductPrice> productPriceList)
        {
            List<ProductPriceDTO> productPriceDTOList = new List<ProductPriceDTO>();
            foreach (ProductPrice productPrice in productPriceList)
            {
                productPriceDTOList.Add
                (
                    new ProductPriceDTO()
                    {
                        ProductCode = productPrice.ProductCode,
                        FixedPrice = productPrice.FixedPrice,
                    }
                );
            }
            return productPriceDTOList;
        }

        //Convert Product Price from Database to ProductPriceDTO 
        public static ProductPriceDTO MapProductDatabase_To_DTO(ProductPrice productPrice)
        {
            ProductPriceDTO productPriceDTO =
                     new ProductPriceDTO()
                     {
                         ProductCode = productPrice.ProductCode,
                         FixedPrice = productPrice.FixedPrice,
                     };
               
            return productPriceDTO;
        }
    }
}

