using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;

namespace CRM_PricingBooks.Database
{
    public class PricingBookDB : IPricingBookDB
    {
        public List<PricingBook> GetAll()
        {
            return new List<PricingBook>()
            {
                new PricingBook()
                { 
                    Name="Lista de precios 2020", 
                    Description="Lista de precios de la gestión 2020", 
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
                    Name="Lista de precios 2019", 
                    Description="Lista de precios de la gestión 2019", 
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
                },
                new PricingBook()
                { 
                    Name="Lista de precios 2018", 
                    Description="Lista de precios de la gestión 2018", 
                    ProductsList = new List<ProductPrice>()
                    {
                        new ProductPrice(){ProductCode="SOCCER-001", FixedPrice=180},
                        new ProductPrice(){ProductCode="SOCCER-002", FixedPrice=160},
                        new ProductPrice(){ProductCode="SOCCER-003", FixedPrice=250},
                        new ProductPrice(){ProductCode="SOCCER-004", FixedPrice=220},
                        new ProductPrice(){ProductCode="BASKET-001", FixedPrice=300},
                        new ProductPrice(){ProductCode="BASKET-002", FixedPrice=120},
                        new ProductPrice(){ProductCode="BASKET-003", FixedPrice=240},
                        new ProductPrice(){ProductCode="BASKET-004", FixedPrice=250}
                    }
                },
                new PricingBook()
                { 
                    Name="Lista de precios 2017", 
                    Description="Lista de precios de la gestión 2017", 
                    ProductsList = new List<ProductPrice>()
                    {
                        new ProductPrice(){ProductCode="SOCCER-001", FixedPrice=170},
                        new ProductPrice(){ProductCode="SOCCER-002", FixedPrice=155},
                        new ProductPrice(){ProductCode="SOCCER-003", FixedPrice=240},
                        new ProductPrice(){ProductCode="SOCCER-004", FixedPrice=215},
                        new ProductPrice(){ProductCode="BASKET-001", FixedPrice=290},
                        new ProductPrice(){ProductCode="BASKET-002", FixedPrice=115},
                        new ProductPrice(){ProductCode="BASKET-003", FixedPrice=235},
                        new ProductPrice(){ProductCode="BASKET-004", FixedPrice=240}
                    }
                },
                new PricingBook()
                { 
                    Name="Lista de precios 2016", 
                    Description="Lista de precios de la gestión 2016", 
                    ProductsList = new List<ProductPrice>()
                    {
                        new ProductPrice(){ProductCode="SOCCER-001", FixedPrice=165},
                        new ProductPrice(){ProductCode="SOCCER-002", FixedPrice=140},
                        new ProductPrice(){ProductCode="SOCCER-003", FixedPrice=240},
                        new ProductPrice(){ProductCode="SOCCER-004", FixedPrice=210},
                        new ProductPrice(){ProductCode="BASKET-001", FixedPrice=285},
                        new ProductPrice(){ProductCode="BASKET-002", FixedPrice=110},
                        new ProductPrice(){ProductCode="BASKET-003", FixedPrice=230},
                        new ProductPrice(){ProductCode="BASKET-004", FixedPrice=240}
                    }
                }
            };
        }
    }
    
}