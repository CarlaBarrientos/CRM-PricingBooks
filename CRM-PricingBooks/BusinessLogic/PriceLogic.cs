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

                //Update(1,"1", "1",pricesLists, listPB);
            }
            return pricesLists;

        }
        private void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook listPB)
        {
            List<PricingBook> allProducts = _productTableDB.GetAll();
            List<PricingBook> filteredList = allProducts.Where(x => (x.Status == true)).ToList();

            if(filteredList.Count > 0 && pricesLists.Count == 0)
            {
                
                foreach (PricingBook pb in filteredList)
                {
                    pricesLists.Add(new PricingBookDTO()
                    {
                        Id = listPB.Id,
                        Name = pb.Name,
                        Description = pb.Description,
                        Status = pb.Status,          //add field status, fill it depending if it's active or not 
                        ProductPrices = pb.ProductsList.ConvertAll(product => new ProductPriceDTO
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice,
                            PromotionPrice = calculatediscount("XMAS", product.FixedPrice)//change this price if there is any active campaign
                        })
                    });
                }
            }

        }

        private void deleteProduct(int id, List<PricingBookDTO>pricesLists)
        {

            foreach (PricingBookDTO product in pricesLists)
            {
                if (product.Id == id)
                {
                    pricesLists.Remove(product);
                    break;
                }
            }

        }
        private void Update(int id, string Name, string Description, List<PricingBookDTO> pricesLists)
        {


            foreach (PricingBookDTO product in pricesLists)
            {
                if (product.Id == id)
                {
                    product.Name = Name;
                    product.Description = Description;
                    break;
                }
            }
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
