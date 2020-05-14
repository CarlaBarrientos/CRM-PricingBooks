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
using Services.Exceptions;

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
            //there should be a try catch here, as it could fail getting the path or lose contact with it
            _dbPath = _configuration.GetSection("Database").GetSection("connectionString").Value;
            _dbContainer = JsonConvert.DeserializeObject<DBContext>(File.ReadAllText(_dbPath));
            Log.Logger.Information("Connection to Database succesful");
            _pricingBookList = _dbContainer.PricingBooks;
        }

        public void SaveChanges()
        {
            File.WriteAllText(_dbPath, JsonConvert.SerializeObject(_dbContainer));
        }

        //***************************CRUD PRICINGBOOKS*************************

        public PricingBook AddNew(PricingBook newPricingBook)
        {
            try { 
                _pricingBookList.Add(newPricingBook);
                SaveChanges();
                Log.Logger.Information("Added new PricingBook: "+ newPricingBook.Id + " succesfully");
                return newPricingBook;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List Pricing Book failed to be added");
                throw new BackingServiceException("Error while adding new ListPricingBook, " + ex.Message);
            }
        }

        public PricingBook Update(PricingBook pricingbookToUpdate, string id)
        {
            try { 
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
                Log.Logger.Information("Updated PricingBook: " + pricingbookToUpdate.Id + " succesfully");
                return pricingbookToUpdate;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to update");
                throw new BackingServiceException("Error while updating listproduct, " + ex.Message);
            }
        }
        public void Delete(string id)
        {
            try
            {
                //Possible new method, erase if not agreed
                /* PricingBook pricingBookToDelete = _pricingBookList.Find
                (
                    pricingBook => pricingBook.Id == id
                );
                if (string.IsNullOrEmpty(pricingBookToDelete.Id))
                {
                    Log.Logger.Information("PricingBook with id: " + id + " is non existent, no changes done");
                }
                else
                {
                    _pricingBookList.Remove(pricingBookToDelete);
                    SaveChanges();
                    Log.Logger.Information("PricingBook with id: " + id + " was deleted succesfully");
                }
               */
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        _pricingBookList.Remove(pb);
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was deleted succesfully");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while deleting List products: " + ex.Message);
                throw new BackingServiceException("Error while deleting ListProduct, " + ex.Message);
            }
        }
        public void Activate(string id)
        {
            try { 
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        pb.Status = true;
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was activated succesfully");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to activate");
                throw new BackingServiceException("Error while activating list, " + ex.Message);
            }
        }

        public void DeActivate(string id)
        {
            try { 
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        pb.Status = false;
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was deactivated succesfully");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("List with id: " + id + " failed to deactivate");
                throw new BackingServiceException("Error while trying to deactivateList, " + ex.Message);
            }
        }

        public List<PricingBook> GetAll()
        {
            return _pricingBookList;
        }

        //***************************CRUD PRODUCTS*************************
        public PricingBook AddNewProduct(List<ProductPrice> newProduct, string id)
        {
            try { 
                PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
                if(pricingBook != null)
                {
                    foreach(ProductPrice product in newProduct)
                    {
                        pricingBook.ProductsList.Add(product);
                        Log.Logger.Information("New Product was added on PricingBook with ProductCode: " + product.ProductCode);
                    }
                    SaveChanges();
                    Log.Logger.Information("Product List was added on PricingBook with id: " + id + " succesfully");
                }
            
                return pricingBook;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Add List product with id: " + id);
                throw new BackingServiceException("Error while adding the newproduct" + ex.Message);
            }
        }
        
        public void DeleteProduct(string code)
        {
            try { 
                PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
                    foreach (ProductPrice productprice in GetProducts(code))
                    {
                        pricingbook.ProductsList.Remove(productprice);
                        SaveChanges();
                    }
                Log.Logger.Information("Product with code: "+ code + " was deleted succesfully");
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Delete product by id with code: " + code);
                throw new BackingServiceException("error while deleting product" + ex.Message);
            }
        }
        public void DeleteProductCode(string code,string productcode)
        {
            try { 
                PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
                if(pricingbook != null)
                {
                    foreach (ProductPrice productprice in GetProducts(code))
                    {
                        if(productprice.ProductCode.Equals(productcode)){
                           pricingbook.ProductsList.Remove(productprice);
                           SaveChanges();
                           Log.Logger.Information("Product from PricingBook.code: " + code + " and product code: " + productcode + " was deleted succesfully");
                        }
                    }
                    Log.Logger.Information("Product with code: " + productcode + " was deleted succesfully from DB");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Delete product by id with productcode: " + productcode);
                throw new BackingServiceException("error while updating product by id" + ex.Message);
            }

        }

        public List<ProductPrice> GetProducts(string id)
        {
            try
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
                Log.Logger.Information("GetProduct : " + id + " succesfull");
                return products;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Get List products with id: " + id);
                throw new BackingServiceException("Error while getting products" + ex.Message);
            }
        }
        public PricingBook UpdateProduct(List <ProductPrice> ppToUpdate, string id)
        {
            try { 
            PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
            if (pricingBook != null)
            {
                pricingBook.ProductsList = ppToUpdate;
                SaveChanges();
                Log.Logger.Information("PricingBook: " + id + " was updated succesfully");
            }
            else
            {
                Log.Logger.Information("PricingBook: " + id + " was not found, no update realized");
            }

            return pricingBook;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Update product with id: " + id);
                throw new BackingServiceException("error while updating product" + ex.Message);
            }
        }
    }

}
