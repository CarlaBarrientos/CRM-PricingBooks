using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.Database;
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


        public PricingBookDBManager(IConfiguration config, ILogger<PricingBookDBManager> logger)
        {
            _configuration = config;
            _logger = logger;
            InitDBContext();
        }
        public void InitDBContext()
        {
            try
            {
                _dbPath = _configuration.GetSection("Database").GetSection("connectionString").Value;
                _dbContainer = JsonConvert.DeserializeObject<DBContext>(File.ReadAllText(_dbPath));
                Log.Logger.Information("Connection to a Database with path: "+_dbPath +" was succesful.");
                _pricingBookList = _dbContainer.PricingBooks;
            }
            catch (Exception) {
                Log.Logger.Error("Connection to Database with path: " + _dbPath+ " is not working");
                throw new DatabaseException("Connection to Database is not working");
            }
        }

        public void SaveChanges()
        {
            File.WriteAllText(_dbPath, JsonConvert.SerializeObject(_dbContainer));
        }

        //***************************CRUD PRICINGBOOKS*************************

        public PricingBook AddNew(PricingBook newPricingBook)
        {
            try
            {
                _pricingBookList.Add(newPricingBook);
                SaveChanges();
                Log.Logger.Information("Added new PricingBook: " + newPricingBook.Id + " succesfully");
                return newPricingBook;
            }
            catch(Exception ex)
            {
                Log.Logger.Error("Error while adding " + newPricingBook.Id + " to Database. " );
                throw new BackingServiceException("Error while adding new ListPricingBook, " + ex.Message);
                //throw new DatabaseException("Error while adding a new Pricing Book to Database.");
            }

        }

        public PricingBook Update(PricingBook pricingbookToUpdate, string id)
        {
            try
            {
                PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));

                //verifying wich fields have to be updated
                if (pricingBook != null)
                {
                    pricingbookToUpdate.Id = id;

                    if (string.IsNullOrEmpty(pricingbookToUpdate.Name))
<<<<<<< HEAD
                    {
                        pricingbookToUpdate.Name = pricingBook.Name;
                    }
                    else
                    {
                        pricingBook.Name = pricingbookToUpdate.Name;
                    }
                    if (string.IsNullOrEmpty(pricingbookToUpdate.Description))
                    {
=======
                    {
                        pricingbookToUpdate.Name = pricingBook.Name;
                    }
                    else
                    {
                        pricingBook.Name = pricingbookToUpdate.Name;
                    }
                    if (string.IsNullOrEmpty(pricingbookToUpdate.Description))
                    {
>>>>>>> 9a520414443d64221d77052c2e39ce6d43f36ca3
                        pricingbookToUpdate.Description = pricingBook.Description;
                    }
                    else
                    {
                        pricingBook.Description = pricingbookToUpdate.Description;
                    }
                    pricingbookToUpdate.Status = pricingBook.Status;
                    if (pricingbookToUpdate.ProductsList.Count() != 0)
                    {
                        pricingBook.ProductsList = pricingbookToUpdate.ProductsList.ConvertAll(product => new ProductPrice
<<<<<<< HEAD
                        {
                            ProductCode = product.ProductCode,
                            FixedPrice = product.FixedPrice
                        });
                    }


                    SaveChanges();
                    Log.Logger.Information("Updated PricingBook: " + pricingbookToUpdate.Id + " succesfully.");
                    return pricingbookToUpdate;
                }
                Log.Logger.Error("The " + id + " does not exist in Database. ");
                  throw new DatabaseException("The Pricing Book does not exist in Database.");
               
=======
                       {
                                ProductCode = product.ProductCode,
                                FixedPrice = product.FixedPrice
                            });
                    }
                }
                SaveChanges();
                Log.Logger.Information("Updated PricingBook: " + pricingbookToUpdate.Id + " succesfully.");
                return pricingbookToUpdate;
>>>>>>> 9a520414443d64221d77052c2e39ce6d43f36ca3
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while updating " + pricingbookToUpdate.Id  +"  to Database. ");
                throw new BackingServiceException("Error while updating listproduct, " + ex.Message);
               // throw new DatabaseException("Error while updating a Pricing Book to Database.");
            }
        }

        public void Delete(string id)
        {
            try
            {
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        _pricingBookList.Remove(pb);
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was deleted succesfully.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while deleting the PricingBook with id: " + id + "  in Database.");
                throw new BackingServiceException("Error while deleting ListProduct, " + ex.Message);
               // throw new DatabaseException("Error while deleting a Pricing Book to Database.");
            }

        }
        public void Activate(string id)
        {
            try
            {
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        pb.Status = true;
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was activated succesfully.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while activating PricingBook with id: " + id+ "  in Database.");
                throw new BackingServiceException("Error while activating list, " + ex.Message);
               // throw new DatabaseException("Error while activating a Pricing Book to Database." );
            }
        }

        public void DeActivate(string id)
        {
            try
            {
                foreach (PricingBook pb in _pricingBookList)
                {
                    if (pb.Id.Equals(id))
                    {
                        pb.Status = false;
                        SaveChanges();
                        Log.Logger.Information("PricingBook with id: " + id + " was deactivated succesfully.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while deactivating PricingBook with id: " + id + "  in Database.");
                throw new BackingServiceException("Error while trying to deactivateList, " + ex.Message);
               // throw new DatabaseException("Error while deactivating a Pricing Book to Database.");
            }
        }

        public List<PricingBook> GetAll()
        {
            try
            {
                return _pricingBookList;
            }
            catch (Exception )
            {
                Log.Logger.Error("Error while getting all PricingBooks from Database." );
                throw new DatabaseException("Error while while getting all PricingBook from Database." );
            }
        }

        //***************************CRUD PRODUCTS*************************
        public PricingBook AddNewProduct(List<ProductPrice> newProduct, string id)
        {
            try
            {
                PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
                if (pricingBook != null)
                {
                    foreach (ProductPrice product in newProduct)
                    {
                        pricingBook.ProductsList.Add(product);
                        Log.Logger.Information("New Product was added on PricingBook with ProductCode: " + product.ProductCode);
                    }
                    SaveChanges();
                    Log.Logger.Information("Product List was added on PricingBook with id: " + id + " succesfully");
                    return pricingBook;
                }
                Log.Logger.Error("The " + id + " does not exist in Database. ");
                throw new DatabaseException("The Pricing Book does not exist in Database.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while adding a new product in the:  " + id + "  in Database.");
                throw new BackingServiceException("Error while adding the newproduct" + ex.Message);
                //throw new DatabaseException("Error while deactivating a Pricing Book in Database." );
            }
        }
        
        public void DeleteProduct(string code)
        {
            try
            {
                PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
                if (pricingbook != null)
                {
                    foreach (ProductPrice productprice in GetProducts(code))
                    {
                        pricingbook.ProductsList.Remove(productprice);
                        SaveChanges();
                    }
                    Log.Logger.Information("Product with code: " + code + " was deleted succesfully");
                }
                else
                {
                    Log.Logger.Error("The " + code + " does not exist in Database. ");
                    throw new DatabaseException("The Pricing Book does not exist in Database.");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while deleting a Product list with code:  " + code + "  in Database.");
                throw new BackingServiceException("error while deleting product" + ex.Message);
                //throw new DatabaseException("Error while deleting a Product list in Database. ");
            }
        }
        public void DeleteProductCode(string code,string productcode)
        {
            try
            {
                PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(code));
                if (pricingbook != null)
                {
                    foreach (ProductPrice productprice in GetProducts(code))
                    {
                        if (productprice.ProductCode.Equals(productcode))
                        {
                            pricingbook.ProductsList.Remove(productprice);
                            SaveChanges();
                            Log.Logger.Information("Product from PricingBook.code: " + code + " and product code: " + productcode + " was deleted succesfully");
                        }
                    }
                    Log.Logger.Information("Product with code: " + productcode + " was deleted succesfully from DB");
                }
                else
                {
                    Log.Logger.Error("The " + code + " does not exist in Database. ");
                    throw new DatabaseException("The Pricing Book does not exist in Database.");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Delete product by id with productcode: " + productcode);
                //throw new DatabaseException("Error while deleting a product in a Product List in Database." );
                throw new BackingServiceException("error while updating product by id" + ex.Message);
            }

        }

        public List<ProductPrice> GetProducts(string id)
        {
            try
            {
                PricingBook pricingbook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
                List<ProductPrice> products = new List<ProductPrice>();
                if (pricingbook != null) 
                {
                    foreach (ProductPrice product in pricingbook.ProductsList)
                    {
                        products.Add(product);
                    }
                    Log.Logger.Information("GetProduct : " + id + " succesfull");
                    return products;
                }
                Log.Logger.Error("The " + id + " does not exist in Database. ");
                throw new DatabaseException("The Pricing Book does not exist in Database.");

            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while getting all products from " + id + "  in Database.");
                throw new BackingServiceException("Error while getting products" + ex.Message);
                //throw new DatabaseException("Error while getting all products from a PricingBook in Database. " );
            }
        }
        public PricingBook UpdateProduct(List <ProductPrice> ppToUpdate, string id)
        {
            try
            {
                PricingBook pricingBook = _pricingBookList.FirstOrDefault(d => d.Id.Equals(id));
                if (pricingBook != null)
                {
                    pricingBook.ProductsList = ppToUpdate;
                    SaveChanges();
                    Log.Logger.Information("PricingBook: " + id + " was updated succesfully with the new Product list");
                    return pricingBook;
                }

                Log.Logger.Error("The " + id + " does not exist in Database. ");
                throw new DatabaseException("The Pricing Book does not exist in Database.");

            }
            catch (Exception ex)
            {
                Log.Logger.Error("Failed to Update product with id: " + id);
                //throw new DatabaseException("Error while updating the Product list from a PricingBook in Database.");
                throw new BackingServiceException("error while updating product" + ex.Message);
            }
        }
    }

}
