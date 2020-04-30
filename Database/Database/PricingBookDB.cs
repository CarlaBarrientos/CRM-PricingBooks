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
            PricingBooks = new List<PricingBook>()
            {
                new PricingBook()
                {
                    Id="PricingBook-1",
                    Name="Lista de precios 2020", 
                    Description="Lista de precios de la gestión 2020",
                    Status = false,
                    ProductsList = new List<ProductPrice>()
                    {
                        new ProductPrice(){ProductCode="SOCCER-001", FixedPrice=200},
                        new ProductPrice(){ProductCode="SOCCER-002", FixedPrice=185},
                        new ProductPrice(){ProductCode="SOCCER-003", FixedPrice=310},
                        new ProductPrice(){ProductCode="SOCCER-004", FixedPrice=250},
                        new ProductPrice(){ProductCode="BASKET-001", FixedPrice=330},
                        new ProductPrice(){ProductCode="BASKET-002", FixedPrice=150},
                        new ProductPrice(){ProductCode="BASKET-003", FixedPrice=270},
                        new ProductPrice(){ProductCode="BASKET-004", FixedPrice=300}
                    }
                },
                new PricingBook()
                { 
                    Id="PricingBook-2",
                    Name="Lista de precios 2019", 
                    Description="Lista de precios de la gestión 2019",
                    Status = false,
                    ProductsList = new List<ProductPrice>()
                    {
                        new ProductPrice(){ProductCode="SOCCER-001", FixedPrice=190},
                        new ProductPrice(){ProductCode="SOCCER-002", FixedPrice=175},
                        new ProductPrice(){ProductCode="SOCCER-003", FixedPrice=390},
                        new ProductPrice(){ProductCode="SOCCER-004", FixedPrice=240},
                        new ProductPrice(){ProductCode="BASKET-001", FixedPrice=320},
                        new ProductPrice(){ProductCode="BASKET-002", FixedPrice=140},
                        new ProductPrice(){ProductCode="BASKET-003", FixedPrice=260},
                        new ProductPrice(){ProductCode="BASKET-004", FixedPrice=280}
                    }
                }
            };
        }

        //***************************CRUD PRICINGBOOKS*************************

        public PricingBook AddNew(PricingBook newPricingBook)
        {
            PricingBooks.Add(newPricingBook);
            return newPricingBook;
        }

        public PricingBook Update(PricingBook pricingbookToUpdate, string id)
        {
            var pricingBook = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            //verifying wich fields have to be updated
            if (pricingBook != null)
            {

                //if(string.IsNullOrEmpty(pbToUpdate.Id))
                //{
                    pricingbookToUpdate.Id = id;
                /*}else
                {
                    pb.Id = pbToUpdate.Id;
                }*/
                if(string.IsNullOrEmpty(pricingbookToUpdate.Name))
                {
                    pricingbookToUpdate.Name = pricingBook.Name;
                }else
                {
                    pricingBook.Name = pricingbookToUpdate.Name;
                }
                if(string.IsNullOrEmpty(pricingbookToUpdate.Description))
                {
                    pricingbookToUpdate.Description = pricingBook.Description;
                }else
                {
                    pricingBook.Description = pricingbookToUpdate.Description;
                }
                pricingbookToUpdate.Status = pricingBook.Status;
                if(pricingbookToUpdate.ProductsList.Count() != 0)
                {
                    pricingBook.ProductsList = pricingbookToUpdate.ProductsList.ConvertAll(product => new ProductPrice
                    {
                        ProductCode = product.ProductCode,
                        FixedPrice = product.FixedPrice
                    });
                }
            }
            return pricingbookToUpdate;
        }
        public void Delete(string id)
        {
            int count = 0;

            foreach(PricingBook pb in GetAll())
            {
                if (pb.Id.Equals(id))
                {
                    GetAll().RemoveAt(count);
                    break;
                }
                else
                {
                    count += 1;
                }
            }
        }
        public void Activate(string id)
        {
            int count = 0;

            foreach (PricingBook pb in GetAll())
            {
                if (pb.Id.Equals(id))
                {
                    pb.Status = true;
                    break;
                }
                else
                {
                    count += 1;
                }
            }
        }

        public void DeActivate(string id)
        {

            foreach (PricingBook pb in GetAll())
            {
                if (pb.Id.Equals(id))
                {
                    pb.Status = false;
                    break;
                }
            }
        }

        public List<PricingBook> GetAll()
        {
            return PricingBooks;
        }

        //***************************CRUD PRODUCTS*************************
        public PricingBook AddNewProduct(List<ProductPrice> newProduct, string id)
        {
            var pricingBook = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            if(pricingBook != null)
            {
                foreach(ProductPrice product in newProduct)
                {
                    pricingBook.ProductsList.Add(product);
                }
            }

            return pricingBook;
        }
        
        public void DeleteProduct(string code)
        {          
           var pricingbook = PricingBooks.FirstOrDefault(d => d.Id.Equals(code));
            if(pricingbook != null)
            {
                foreach (ProductPrice productprice in GetProducts(code))
                {
                    pricingbook.ProductsList.Remove(productprice);
                    
                } 
            }
        }
        public void DeleteProductCode(string code,string productcode)
        {          
           var pricingbook = PricingBooks.FirstOrDefault(d => d.Id.Equals(code));
            if(pricingbook != null)
            {
                foreach (ProductPrice productprice in GetProducts(code))
                {
                    if(productprice.ProductCode.Equals(productcode)){
                       pricingbook.ProductsList.Remove(productprice);

                    }
                    
                } 
            }
        }

        public List<ProductPrice> GetProducts(string id)
        {
            var pricingbook = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            List<ProductPrice> products = new List<ProductPrice>();
            if(pricingbook != null)
            {
                foreach (ProductPrice product in pricingbook.ProductsList)
                {
                    products.Add(product);

                }
            }

            return products;
        }
        public PricingBook UpdateProduct(List <ProductPrice> ppToUpdate, string id)
        {

            PricingBook pricingBook = PricingBooks.FirstOrDefault(d => d.Id.Equals(id));
            if (pricingBook != null)
            {
                pricingBook.ProductsList = ppToUpdate;
  
            }

            return pricingBook;
        }
    }

}
