using System;
using System.Collections.Generic;
using System.Linq;

using Services;
using CRM_PricingBooks.DTOModels;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.Database.Models;


namespace CRM_PricingBooks.BusinessLogic
{
    public class PriceLogic : IPriceLogic
    {
        private int code = 0;
        private readonly IPricingBookDB _productTableDB;
        private readonly ICampaignBackingService _campaign;
        
        public PriceLogic(IPricingBookDB productTableDB, ICampaignBackingService campaignDB)
        {
            _productTableDB = productTableDB;
            _campaign = campaignDB;

        }
        
        

        public List<PricingBookDTO> GetPricingBooks() { //Reads all Pricing Books, also updates their price based on the active campaign.

            List<PricingBook> allProducts = _productTableDB.GetAll();
            List<PricingBook> filteredListTrue = allProducts.Where(x => (x.Status == true)).ToList();//filtering active list
            List<PricingBook> filteredListFalse = allProducts.Where(x => (x.Status == false)).ToList();//filtering deactivate lists
            List<PricingBookDTO> pricesLists = new List<PricingBookDTO>();

            if(filteredListTrue.Count > 0 && pricesLists.Count == 0)
            {
                foreach (PricingBook pricingBook in filteredListTrue)
                {
                    fillPriceList(pricesLists, pricingBook);//filling active price list calculating discounts

                }
            }

            //Mapping PricingBook => PricingBookDTO
            foreach (PricingBook pricingBook in filteredListFalse)
            {
                pricesLists.Add(new PricingBookDTO()
                {
                    Id = pricingBook.Id.ToString(),
                    Name = pricingBook.Name,
                    Description = pricingBook.Description,
                    Status = pricingBook.Status,
                    ProductPrices = pricingBook.ProductsList.ConvertAll(product => new ProductPriceDTO
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice,
                        PromotionPrice = product.FixedPrice
                    })
                });

            }           

            return pricesLists;

        }
        
        private void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook pricingBook)
        {
            //Here i recover the active campaign to calculate the discounts
            
            List<CampaignBSDTO> campaigns = _campaign.GetAllCampaign().Result;

            pricesLists.Add(new PricingBookDTO()
            {
                Id = pricingBook.Id.ToString(),
                Name = pricingBook.Name,
                Description = pricingBook.Description,
                Status = pricingBook.Status,   
                ProductPrices = pricingBook.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    PromotionPrice = calculatediscount(campaigns[0].Type, product.FixedPrice)
                })
            });
        }
        
        public bool DeleteListProduct(string id)
        {
            List<PricingBookDTO> priceslist = GetPricingBooks();
            foreach (PricingBookDTO pbDTO in priceslist)
            {
                if (pbDTO.Id.Equals(id))
                {
                    priceslist.Remove(pbDTO);
                    _productTableDB.Delete(id);
                    return true;
                }

            }
            return false;
        }
        
        public string ActivateList(string id)
        {
            List<PricingBookDTO> priceslist = GetPricingBooks();
            List<PricingBookDTO> filteredList = priceslist.Where(x => (x.Status == true)).ToList();

            string aux = "";

            if (filteredList.Count > 0)
            {
                aux = "AN EXISTING LIST IS ALREADY ACTIVATED ";
                return aux + filteredList;
            }
            else
            {
                foreach (PricingBookDTO pbDTO in priceslist)
                {
                    if (pbDTO.Id.Equals(id))
                    {
                        pbDTO.Status = true;
                        aux = "ACTIVATING LIST WITH ID " + id;
                        _productTableDB.Activate(id);
                        return aux;
                    }
                }
                return aux;
            }
        }
        public PricingBookDTO UpdateListProduct(PricingBookDTO pricingBookToUpdate, string id)
        {
            PricingBook pbUpdated = new PricingBook();

            //pbUpdated.Id = pricingBookToUpdate.Id;
            pbUpdated.Name = pricingBookToUpdate.Name;
            pbUpdated.Description = pricingBookToUpdate.Description;
            if(pricingBookToUpdate.ProductPrices.Count() != 0)
                {
                    pbUpdated.ProductsList = pricingBookToUpdate.ProductPrices.ConvertAll(product => new ProductPrice
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice
                    });
                }
            
            PricingBook pbInDB = _productTableDB.Update(pbUpdated, id);

            return new PricingBookDTO()
            {
                Id = pbInDB.Id,
                Name = pbInDB.Name,
                Description = pbInDB.Description,
                Status = pbInDB.Status,
                ProductPrices = pbInDB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    PromotionPrice = product.FixedPrice
                })

            };

        }

        public PricingBookDTO AddNewListPricingBook(PricingBookDTO newPricingBook) 
        {
            PricingBook pricingBook = new PricingBook();
            
            
            pricingBook.Name = newPricingBook.Name;
            pricingBook.Description = newPricingBook.Description;
            pricingBook.Id = SelfGenerationID();
            pricingBook.Status = false;
            pricingBook.ProductsList = newPricingBook.ProductPrices.ConvertAll(product => new ProductPrice
            {
                ProductCode = product.ProductCode,
                FixedPrice = product.FixedPrice
            });
            
            PricingBook pricingBookInDB = _productTableDB.AddNew(pricingBook);
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

        public string DeActivateList(string id)
        {
            List<PricingBookDTO> priceslist = GetPricingBooks();

            string deactivationMessage = "";

            foreach (PricingBookDTO pbDTO in priceslist)
            {
                if (pbDTO.Id.Equals(id))
                {
                    pbDTO.Status = false;
                    deactivationMessage = "DEACTIVATING LIST WITH ID " + id;
                    _productTableDB.DeActivate(id);
                    return deactivationMessage;
                }
            }
            return deactivationMessage;
        }

        private string SelfGenerationID()
        {
            List<PricingBook> allProducts = _productTableDB.GetAll();
            
            if(allProducts.Count == 0)
            {
                code = 1;
            }
            else
            {
                string[] division = allProducts[allProducts.Count - 1].Id.Split('-');//getting the id of the last data stored in data base
                code = Int32.Parse(division[1]) + 1;                
            }

            return "PricingBook-"+code.ToString();
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
