using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.DTOModels;





namespace CRM_PricingBooks.BusinessLogic
{
    public interface IPriceLogic
    {
        List<PricingBookDTO> GetPricingBooks();
        PricingBookDTO AddNewListPricingBook(PricingBookDTO newPricingBook);
        PricingBookDTO UpdateListProduct(PricingBookDTO productToUpdate, string id);
        string ActivateList(string id);
        bool DeleteListProduct(string id);
    }
}
