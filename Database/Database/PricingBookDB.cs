using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public class PricingBookDB : IPricingBookDB
    {
        private List<PricingBook> PricingBooks{ get; set; }

        public PricingBookDB()
        {
            PricingBooks = new List<PricingBook>();
        }

        public PricingBook Update(PricingBook pbToUpdate, string id)
        {
            return pbToUpdate;
        }

        public List<PricingBook> GetAll()
        {
            return PricingBooks;
        }

    }
    
}