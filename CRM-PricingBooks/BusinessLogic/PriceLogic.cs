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

            return pricesLists;
        }
    }
}
