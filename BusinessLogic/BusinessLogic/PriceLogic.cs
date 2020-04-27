using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CRM_PricingBooks.DTOModels;
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
        //FUNCIONA------------------------------------------------------
        public List<PricingBookDTO> GetPricingBooks() {

            List<PricingBook> allProducts = _productTableDB.GetAll();
            List<PricingBook> filteredList = allProducts.Where(x => (x.Status == true)).ToList();
            List<PricingBook> filteredListF = allProducts.Where(x => (x.Status == false)).ToList();
            List<PricingBookDTO> pricesLists = new List<PricingBookDTO>();

            if(filteredList.Count > 0 && pricesLists.Count == 0)
            {
                foreach (PricingBook listPB in filteredList)
                {
                    fillPriceList(pricesLists, listPB);

                }
            }

            foreach (PricingBook pb in filteredListF)
            {
                pricesLists.Add(new PricingBookDTO()
                {
                    Id = pb.Id.ToString(),
                    Name = pb.Name,
                    Description = pb.Description,
                    Status = pb.Status,
                    ProductPrices = pb.ProductsList.ConvertAll(product => new ProductPriceDTO
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice,
                        PromotionPrice = product.FixedPrice
                    })
                });

            }           

            return pricesLists;

        }
        //FUNCIONA-------------------------------------------
        private void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook listPB)
        {           
            pricesLists.Add(new PricingBookDTO()
            {
                Id = listPB.Id.ToString(),
                Name = listPB.Name,
                Description = listPB.Description,
                Status = listPB.Status,   
                ProductPrices = listPB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    PromotionPrice = calculatediscount("XMAS", product.FixedPrice)
                })
            });
        }
        //NO FUNCIONA----------------------------------------
        public void DeleteListProduct(int id)
        {
            List<PricingBook> allProducts = _productTableDB.GetAll();
            foreach (PricingBook product in allProducts)
            {
                if (product.Id.Equals(id))
                {
                    allProducts.Remove(product);
                    break;
                }
            }

        }
        //NO FUNCIONA-----------------------------------
        public PricingBookDTO UpdateListProduct(PricingBookDTO pricingBookToUpdate, string id)
        {
            PricingBook pbUpdated = new PricingBook();

            if(string.IsNullOrEmpty(pricingBookToUpdate.Id))
            {
                pbUpdated.Id = null;
            }else
            {
                pbUpdated.Id = pricingBookToUpdate.Id;
            }
            if(string.IsNullOrEmpty(pricingBookToUpdate.Name))
            {
                pbUpdated.Name = null;
            }else
            {
                pbUpdated.Name = pricingBookToUpdate.Name;
            }
            if(string.IsNullOrEmpty(pricingBookToUpdate.Description))
            {
                pbUpdated.Description = null;
            }else
            {
                pbUpdated.Description = pricingBookToUpdate.Description;
            }
            /*if(pricingBookToUpdate.ProductPrices == null)
            {
                pbUpdated.ProductsList = null;
                
            }else
            {*/
                if(pricingBookToUpdate.ProductPrices.Count() != 0)
                {
                    pbUpdated.ProductsList = pricingBookToUpdate.ProductPrices.ConvertAll(product => new ProductPrice
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice
                    });
                }
            //}
            
            Console.WriteLine("estamos en logic");

            PricingBook pbInDB = _productTableDB.Update(pbUpdated, id);

            Console.WriteLine("despues de db");


            return new PricingBookDTO()
            {
                Id = pbInDB.Id,
                Name = pbInDB.Name,
                Description = pbInDB.Description,
                Status = pbInDB.Status,
                ProductPrices = pbInDB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice
                })

            };

        }

        public PricingBookDTO AddNewListPricingBook(PricingBookDTO newPricingBook) 
        {
            PricingBook pricingBook = new PricingBook();
           //ProductPriceDTO productPrice = new ProductPriceDTO();
            
            pricingBook.Name = newPricingBook.Name;
            pricingBook.Description = newPricingBook.Description;
            pricingBook.Id = SelfGenerationID();
            pricingBook.Status = newPricingBook.Status;
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
                    FixedPrice = product.FixedPrice
                })

            };
        }

        public string SelfGenerationID()
        {
            PricingBook pricingBook = new PricingBook();

            List<PricingBook> pricings = new List<PricingBook>();
            pricingBook.Id = Convert.ToString(pricings.Count);

            return pricingBook.Id;

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
