using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_PricingBooks.Database.Models
{
    public class ProductPrice
    {
        public string ProductCode{ get; set; }

        public double FixedPrice{ get; set; }
    }    
}