using CRM_PricingBooks.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Database.Models
{
   public class DBContext
    {
        public List<PricingBook> PricingBooks { get; set; }
       
    }
}
