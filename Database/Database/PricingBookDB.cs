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

        //***************************CRUD PRICINGBOOKS*************************

        public PricingBook AddNew(PricingBook newPricingBook)
        {
            PricingBooks.Add(newPricingBook);
            return newPricingBook;
        }

        public PricingBook Update(PricingBook pbToUpdate, string id)
        {
            var pb = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            if (pb != null) 
            {

                //if(string.IsNullOrEmpty(pbToUpdate.Id))
                //{
                    pbToUpdate.Id = id;
                /*}else
                {
                    pb.Id = pbToUpdate.Id;
                }*/
                if(string.IsNullOrEmpty(pbToUpdate.Name))
                {
                    pbToUpdate.Name = pb.Name;
                }else
                {
                    pb.Name = pbToUpdate.Name;
                }
                if(string.IsNullOrEmpty(pbToUpdate.Description))
                {
                    pbToUpdate.Description = pb.Description;
                }else
                {
                    pb.Description = pbToUpdate.Description;
                }
                pbToUpdate.Status = pb.Status;
                if(pbToUpdate.ProductsList.Count() != 0)
                {                
                    pb.ProductsList = pbToUpdate.ProductsList.ConvertAll(product => new ProductPrice
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice
                    });                          
                }
            }
            return pbToUpdate;
        }

        public List<PricingBook> GetAll()
        {
            return PricingBooks;
        }

        //***************************CRUD PRODUCTS*************************
        public PricingBook AddNewProduct(ProductPrice newProduct, string id)
        {
            var pb = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            if(pb != null)
            {
                pb.ProductsList.Add(newProduct); 
            }

            return pb;
        }

        public List<ProductPrice> GetProducts(string id)
        {
            var pb = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            List<ProductPrice> productos = new List<ProductPrice>();
            if(pb != null)
            {
                foreach (ProductPrice pp in pb.ProductsList)
                {
                    productos.Add(pp);
                    
                } 
            }   

            return productos;
        }
    }
    
}