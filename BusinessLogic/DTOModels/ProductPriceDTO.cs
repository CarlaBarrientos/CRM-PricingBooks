using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_PricingBooks.DTOModels
{
    public class ProductPriceDTO
    {
        public string ProductCode { get; set; }

        public double FixedPrice { get; set; }

        public double PromotionPrice { get; set; }
    }
}
