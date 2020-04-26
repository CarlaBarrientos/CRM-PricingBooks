using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.DTOModels;





namespace CRM_PricingBooks.BusinessLogic
{

    public interface IPriceLogic
    {
        public List<PricingBookDTO> GetPricingBooks();
        public void AddNewListProduct(PricingBookDTO newProduct);
        public PricingBookDTO UpdateListProduct(PricingBookDTO productToUpdate, string id);
        void DeleteListProduct(int id);
    }
}
