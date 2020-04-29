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

        public ProductPriceController(IProductLogic studentLogic)
        {
            _productLogic= studentLogic;
        }

        [HttpGet]
        [Route("pricing-books/{id}/product-prices")]
        public IEnumerable<ProductPriceDTO> GetProducts(string id)
        {
            return _productLogic.GetProducts(id);
        }
        [HttpPost]
        [Route("pricing-books/{id}/product-prices")]
        public PricingBookDTO Post([FromBody]ProductPriceDTO newProductDTO, string id)
        {
            return _productLogic.AddNewProduct(newProductDTO, id);
        }

        // PUT: api/Student/12345
        [HttpPut]
        [Route("pricing-books/{id}/product-prices")]
        public void Put([FromBody]List<ProductPriceDTO> productToUpdate, string id) // id=Code:12345
        {
            _productLogic.UpdateProduct(productToUpdate,id);
        }

        [HttpDelete]
        [Route("pricing-books/{id}/product-prices")]
        public void Delete(string id) // CI:65008816
        {
            _productLogic.DeleteProduct(id);

        }
    }
}
