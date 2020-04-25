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
        void AddNewListProduct(PricingBookDTO newProduct);
        void UpdateListProduct(PricingBookDTO productToUpdate, int id);
        void DeleteListProduct(int id);
    }
}