using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using CRM_PricingBooks.DTOModels;
using CRM_PricingBooks.BusinessLogic;

namespace CRM_PricingBooks.Controllers
{
    [Route("crm")]
    [ApiController]

    public class PricingBookController : ControllerBase
    {
        private readonly IPriceLogic _priceLogic;
        private readonly IConfiguration _configuration;

        public PricingBookController(IPriceLogic pricelogic, IConfiguration config)
        {
            _priceLogic = pricelogic;
            _configuration = config;
        }

        [HttpGet]
        [Route("pricing-books")]
        public IEnumerable<PricingBookDTO> GetAll()
        {
            return _priceLogic.GetPricingBooks();
        }

        [HttpPost]
        [Route("pricing-books")]
        public PricingBookDTO Post([FromBody]PricingBookDTO newPriceBookDTO)
        {
            PricingBookDTO newPriceBook = _priceLogic.AddNewListPricingBook(newPriceBookDTO);

            var dbServer = _configuration.GetSection("Database").GetSection("ServerName");
            newPriceBook.Name = $"{newPriceBook.Name} data from {dbServer.Value}";

            return newPriceBook;
        }

        // PUT: api/Student/12345
        [HttpPut]
        [Route("pricing-books/{id}")]
        public PricingBookDTO Put([FromBody]PricingBookDTO pricingBookToUpdate, string id) // id=Code:12345
        {
            return _priceLogic.UpdateListProduct(pricingBookToUpdate, id);
        }

        [HttpDelete]
        [Route("pricing-books/{id}")]
        public bool Delete(string id) // CI:65008816
        {
            return _priceLogic.DeleteListProduct(id);
        }

        [HttpPost]
        [Route("pricing-books/{id}/activate")]
        public string ActivatePost(string id)
        {
            string active = _priceLogic.ActivateList(id);
            return active;
        }

        [HttpPost]
        [Route("pricing-books/{id}/deactivate")]
        public string DeActivatePost(string id)
        {
            string active = _priceLogic.DeActivateList(id);
            return active;
        }
    }
}