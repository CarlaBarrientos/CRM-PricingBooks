using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public interface IPricingBookDB
    {
        public List<PricingBook> GetAll();
    }
}