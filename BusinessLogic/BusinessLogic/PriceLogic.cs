using System;
using System.Collections.Generic;
using System.Linq;

using Services;
using CRM_PricingBooks.DTOModels;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.Database.Models;
using BusinessLogic.DTOModels;
using Services.Exceptions;
using Serilog;

namespace CRM_PricingBooks.BusinessLogic
{
    public class PriceLogic : IPriceLogic
    {
        private int code = 0;
        private IPricingBookDBManager _productTableDB;
        private readonly ICampaignBackingService campaign;
       
        
        public PriceLogic(IPricingBookDBManager productTableDB, ICampaignBackingService campaignBS)
        {
            _productTableDB = productTableDB;
            campaign = campaignBS;

        }
        
        

        public List<PricingBookDTO> GetPricingBooks() { //Reads all Pricing Books, also updates their price based on the active campaign.
            try
            { 
                List<PricingBook> allProducts = _productTableDB.GetAll();
                List<PricingBook> filteredListTrue = allProducts.Where(x => (x.Status == true)).ToList();//filtering active list
                List<PricingBook> filteredListFalse = allProducts.Where(x => (x.Status == false)).ToList();//filtering deactivate lists
                List<PricingBookDTO> pricesLists = new List<PricingBookDTO>();
                if (filteredListTrue.Count > 0 && pricesLists.Count == 0)
                {
                    foreach (PricingBook pricingBook in filteredListTrue)
                    {
                        fillPriceList(pricesLists, pricingBook);//filling active price list calculating discounts
                    }
                }
            
                List<CampaignBSDTO> campaigns = campaign.GetAllCampaigns().Result;
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
                            PromotionPrice = calculatediscount(campaigns[0].Type, product.FixedPrice)
                        })
                    });

                }
                return pricesLists;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while getting PricingBooks: " + ex.Message);
                throw new BackingServiceException("Error while getting PricingBooks: " + ex.Message);
            }
        }
        
        private void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook pricingBook)
        {
            //Here i recover the active campaign to calculate the discounts

            
            List<CampaignBSDTO> campaigns = campaign.GetAllCampaigns().Result;
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
            try { 
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
                Log.Logger.Information("PricingBook with id: " + id + "is not existent");
                return false;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while deleting List products: " + ex.Message);
                throw new BackingServiceException("Error while deleting ListProduct, " + ex.Message);
            }
        }
        
        public string ActivateList(string id)
        {
            try{
                List<PricingBookDTO> priceslist = GetPricingBooks();
                List<PricingBookDTO> filteredList = priceslist.Where(x => (x.Status == true)).ToList();

                string aux = "";

                if (filteredList.Count > 0)
                {
                    aux = "AN EXISTING LIST IS ALREADY ACTIVATED ";
                    Log.Logger.Information("List with id: " + id + " is already active");
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
                            Log.Logger.Information("List with id: " + id + " was activated succesfully");
                            return aux;
                        }
                    }
                    return aux;
                }   
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to activate");
                throw new BackingServiceException("Error while activating list, " + ex.Message);
            }
        }
        public PricingBookDTO UpdateListProduct(PricingBookDTO pricingBookToUpdate, string id)
        {
            try {
                PricingBook pbUpdated = new PricingBook
                {
                    Id = id,
                    Name = pricingBookToUpdate.Name,
                    Description = pricingBookToUpdate.Description
                };
                if (pricingBookToUpdate.ProductPrices.Count() != 0)
                    {
                        pbUpdated.ProductsList = pricingBookToUpdate.ProductPrices.ConvertAll(product => new ProductPrice
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice
                        });
                    }

                PricingBook pbInDB = _productTableDB.Update(pbUpdated, id);
                return DTOUtil.MapPricingBookDatabase_To_DTO(pbInDB);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to update");
                throw new BackingServiceException("Error while updating listproduct, " + ex.Message);
            }
        }

        public PricingBookDTO AddNewListPricingBook(PricingBookDTO newPricingBook)
        {
            try { 
                PricingBook pricingBook = new PricingBook
                {
                    Name = newPricingBook.Name,
                    Description = newPricingBook.Description,
                    Id = SelfGenerationID(),
                    Status = false,
                    ProductsList = newPricingBook.ProductPrices.ConvertAll(product => new ProductPrice
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice
                    })
                };            
                PricingBook pricingBookInDB = _productTableDB.AddNew(pricingBook);

                return DTOUtil.MapPricingBookDatabase_To_DTO(pricingBookInDB);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List Pricing Book failed to be added");
                throw new BackingServiceException("Error while adding new ListPricingBook, " + ex.Message);
            }
        }

        public string DeActivateList(string id)
        {
            try { 
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
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to deactivate");
                throw new BackingServiceException("Error while trying to deactivateList, " + ex.Message);
            }
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
