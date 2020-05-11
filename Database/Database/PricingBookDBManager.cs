using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database.Models;
using Database.Database.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace CRM_PricingBooks.Database
{
    public class PricingBookDBManager : IPricingBookDBManager
    {
        private List<PricingBook> _pricingBookList;

        private readonly IConfiguration _configuration;
        private string _dbPath;
        private DBContext _dbContainer;
        public readonly ILogger<PricingBookDBManager> _logger;


        public PricingBookDBManager(IConfiguration config)
        {
            _configuration = config;
            InitDBContext();
        }
        public void InitDBContext()
        {
            _dbPath = _configuration.GetSection("Database").GetSection("connectionString").Value;
            //Log.Logger.Information("test");
            _dbContainer = JsonConvert.DeserializeObject<DBContext>(File.ReadAllText(_dbPath));
            _pricingBookList = _dbContainer.PricingBooks;
        }

        public void SaveChanges()
        {
            File.WriteAllText(_dbPath, JsonConvert.SerializeObject(_dbContainer));
        }

        //***************************CRUD PRICINGBOOKS*************************

        public PricingBook AddNew(PricingBook newPricingBook)
        {
            _pricingBookList.Add(newPricingBook);
            SaveChanges();
            return newPricingBook;
        }

        public PricingBook Update(PricingBook pricingbookToUpdate, string id)
        {
            PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
            //verifying wich fields have to be updated
            if (pricingBook != null)
            {
                    pricingbookToUpdate.Id = id;
                
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
           
            SaveChanges();
            return pricingbookToUpdate;
        }
        public void Delete(string id)
        {
            foreach(PricingBook pb in _pricingBookList)
            {
                if (pb.Id.Equals(id))
                {
                    _pricingBookList.Remove(pb);
                    SaveChanges();
                    break;
                }
            }
            
        }
        public void Activate(string id)
        {
            foreach (PricingBook pb in _pricingBookList)
            {
                if (pb.Id.Equals(id))
                {
                    pb.Status = true;
                    SaveChanges();
                    break;
                }
            }
        }

        public void DeActivate(string id)
        {

            foreach (PricingBook pb in _pricingBookList)
            {
                if (pb.Id.Equals(id))
                {
                    pb.Status = false;
                    SaveChanges();
                    break;
                }
            }
        }

        public List<PricingBook> GetAll()
        {
            return _pricingBookList;
        }

        //***************************CRUD PRODUCTS*************************
        public PricingBook AddNewProduct(List<ProductPrice> newProduct, string id)
        {
            PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
            if(pricingBook != null)
            {
                foreach(ProductPrice product in newProduct)
                {
                    pricingBook.ProductsList.Add(product);
                    
                }
                SaveChanges();
            }
            
            return pricingBook;
        }
        
        public void DeleteProduct(string code)
        {
            PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
           
                foreach (ProductPrice productprice in GetProducts(code))
                {
                    pricingbook.ProductsList.Remove(productprice);
                    SaveChanges();
                } 
            
        }
        public void DeleteProductCode(string code,string productcode)
        {
            PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
            if(pricingbook != null)
            {
                foreach (ProductPrice productprice in GetProducts(code))
                {
                    if(productprice.ProductCode.Equals(productcode)){
                       pricingbook.ProductsList.Remove(productprice);
                       SaveChanges();
                    }
                    
                } 
            }
        }

        public List<ProductPrice> GetProducts(string id)
        {
            PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
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

            PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
            if (pricingBook != null)
            {
                pricingBook.ProductsList = ppToUpdate;
                SaveChanges();
            }

            return pricingBook;
        }
    }

}
