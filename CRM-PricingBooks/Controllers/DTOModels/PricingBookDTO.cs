using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_PricingBooks.Controllers.DTOModels
{
    public class PricingBookDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public List<ProductPriceDTO> ProductPrices { get; set; }
    }
}
