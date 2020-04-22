using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Controllers.DTOModels;

using CRM_PricingBooks.Database.Models;



namespace CRM_PricingBooks.BusinessLogic
{

    public interface IPriceLogic
    {
        public List<PricingBookDTO> GetPricingBooks();
        //public void deleteProduct(int id, List<PricingBookDTO> pricesLists);
    }
}