using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_PricingBooks.Database.Models
{
    public class PricingBook
    {
        public string Name{ get; set; }

        public string Description{ get ; set; }

        public List<ProductPrice> ProductsList{ get; set; }
    }    
}