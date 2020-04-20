using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Controllers.DTOModels;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.Database.Models;


namespace CRM_PricingBooks.BusinessLogic
{
    public class PriceLogic : IPriceLogic
    {
        private readonly IPricingBookDB _productTableDB;

        public PriceLogic(IPricingBookDB productTableDB)
        {
            _productTableDB = productTableDB;
        }

        public List<PricingBookDTO> GetPricingBooks() {

            List<PricingBook> allProducts = _productTableDB.GetAll(); //Retreive all books from databse

            List<PricingBookDTO> pricesLists = new List<PricingBookDTO>();

           foreach (PricingBook listPB in allProducts)
         {
                fillPriceList(pricesLists, listPB);
         }

            return pricesLists;
        }
        public void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook listPB)
        {
            pricesLists.Add(new PricingBookDTO()
            {
                Name = listPB.Name,
                Description = listPB.Description,
                //add field status, fill it depending if it's active or not
                ProductPrices = listPB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    PromotionPrice = calculatediscount("XMAS", product.FixedPrice)//change this price if there is any active campaign
                })
            });

        }
     private double calculatediscount(String activeCampaign, Double price) //Calculating discounts
    {

      if(activeCampaign == "XMAS")
      {
        price = price - price * (0.05);

       }
      if(activeCampaign == "SUMMER")
      {
        price = price - price * (0.20);
      }
      if(activeCampaign == "BFRIDAY")
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
