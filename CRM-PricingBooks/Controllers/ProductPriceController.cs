using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using CRM_PricingBooks.BusinessLogic;
using CRM_PricingBooks.DTOModels;
using Microsoft.Extensions.Logging;

namespace CRM_PricingBooks.Controllers
{
    [Route("crm")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
                private readonly IConfiguration _configuration;



        public ProductPriceController(IProductLogic studentLogic, IConfiguration config)
        {
            _productLogic= studentLogic;
            _configuration = config;

        }

        [HttpGet]
        [Route("products")]
        public List<ProductPriceDTO> GetAll()
        {


            return _productLogic.GetAll();
        }
        [HttpPost]
        [Route("products")]
        public ProductPriceDTO Post([FromBody]ProductPriceDTO newProductDTO)
        {

            ProductPriceDTO  productoprice=_productLogic.AddNewProduct(newProductDTO);
             var dbServer = _configuration.GetSection("Database").GetSection("ServerName");
            productoprice.ProductCode = $"{productoprice.ProductCode} data from {dbServer.Value}";

            return productoprice;

        }

        // PUT: api/Student/12345
        [HttpPut]
        [Route("products/{id}")]
        public ProductPriceDTO Put([FromBody]ProductPriceDTO productToUpdate, string id) // id=Code:12345
        {
             return _productLogic.UpdateProduct(productToUpdate,id);
        }

        [HttpDelete]
        [Route("products/{id}")]
        public void Delete(string id) // CI:65008816
        {
            _productLogic.DeleteProduct(id);

        }
    }
}
