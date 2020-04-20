using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Controllers.DTOModels; 

namespace CRM_PricingBooks.BusinessLogic
{

    public interface IPriceLogic
    {
        public List<PricingBookDTO> GetPricingBooks();
    }
}