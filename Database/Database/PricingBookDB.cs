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

        public void AddNew(PricingBook newPricingBook)
        {

        }

        public PricingBook Update(PricingBook pbToUpdate, string id)
        {
            Console.WriteLine("estamos en db");
            var pb = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            if (pb != null) 
            {
                Console.WriteLine("estamos en db2");
                if(pb.Id.Equals(id))
                {
                    if(string.IsNullOrEmpty(pbToUpdate.Id))
                    {
                        Console.WriteLine("estamos en id");
                        pbToUpdate.Id = id;
                    }else
                    {
                        pb.Id = pbToUpdate.Id;
                    }
                    if(string.IsNullOrEmpty(pbToUpdate.Name))
                    {
                        Console.WriteLine("estamos en name");
                        pbToUpdate.Name = pb.Name;
                    }else
                    {
                        pb.Name = pbToUpdate.Name;
                    }
                    if(string.IsNullOrEmpty(pbToUpdate.Description))
                    {
                        Console.WriteLine("estamos en description   ");
                        pbToUpdate.Description = pb.Description;
                    }else
                    {
                        pb.Description = pbToUpdate.Description;
                    }
                    /*if(pbToUpdate.ProductsList == null)
                    {
                        pbToUpdate.ProductsList = pb.ProductsList.ConvertAll(product => new ProductPrice
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice
                        });  
                        
                    }else
                    {*/
                        if(pbToUpdate.ProductsList.Count() != 0)
                        {
                            Console.WriteLine("estamos en lista productos");
                        
                            foreach(ProductPrice product in pbToUpdate.ProductsList)
                            {
                                pb.ProductsList.Add(new ProductPrice
                                {
                                    ProductCode = product.ProductCode,
                                    FixedPrice = product.FixedPrice
                                });
                            }

                            pbToUpdate.ProductsList = pb.ProductsList.ConvertAll(product => new ProductPrice
                            {
                                ProductCode = product.ProductCode,
                                FixedPrice = product.FixedPrice
                            });                          
                        }
                    //}
                    
                    pbToUpdate.Status = pb.Status;
                }
            }
            return pbToUpdate;
        }

        public List<PricingBook> GetAll()
        {
            return PricingBooks;
        }

    }
    
}