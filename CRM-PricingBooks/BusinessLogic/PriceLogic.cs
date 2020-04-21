using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database;
using CRM_PricingBooks.Database.Models;
using CRM_PricingBooks.Controllers.DTOModels;


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

            List<PricingBook> allProducts = _productTableDB.GetAll();

            List<PricingBookDTO> pricesLists = new List<PricingBookDTO>();

            foreach (PricingBook listPB in allProducts)
            {
                fillPriceList(pricesLists, listPB);
                //Update(1,"1", "1",pricesLists, listPB);
            }


            return pricesLists;
        }

        public void fillPriceList(List<PricingBookDTO> pricesLists, PricingBook listPB)
        {
            pricesLists.Add(new PricingBookDTO()
            {
                Id = listPB.Id,
                Name = listPB.Name,
                Description = listPB.Description,
                //add field status, fill it depending if it's active or not 
                ProductPrices = listPB.ProductsList.ConvertAll(product => new ProductPriceDTO
                {
                    ProductCode = product.ProductCode,
                    FixedPrice = product.FixedPrice,
                    //PromotionPrice = product.FixedPrice//change this price if there is any active campaign
                })
            });

        }
        public void deleteProduct(int id, List<PricingBookDTO> pricesLists, PricingBook listPB)
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
        public void Update(int id, string Name, string Description, List<PricingBookDTO> pricesLists, PricingBook listPB)
        {
            foreach (PricingBookDTO product in pricesLists)
            {
                if (product.Id == id)
                {
                    product.Name = Name;
                    product.Description =Description;
                    break;
                }
            }
        }
       
        private PricingBook readListProduct(List<PricingBook> listProducts, PricingBook productR)
        {
            PricingBook showProduct = null;
            foreach (PricingBook product in listProducts)
            {
                if (product.Equals(productR))
                {
                    showProduct = product;
                }
            }
            return showProduct;
        }

    }
}
