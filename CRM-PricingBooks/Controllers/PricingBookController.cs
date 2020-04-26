using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CRM_PricingBooks.DTOModels;
using CRM_PricingBooks.BusinessLogic;

namespace CRM_PricingBooks.Controllers
{
    [Route("crm")]
    [ApiController]

    public class PricingBookController : ControllerBase
    {
        private readonly IPriceLogic _priceLogic;

        public PricingBookController(IPriceLogic pricelogic)
        {
            _priceLogic = pricelogic;
        }

        [HttpGet]
        [Route("pricingbooks")]
        public IEnumerable<PricingBookDTO> GetAll()
        {
            return _priceLogic.GetPricingBooks();
        }

        [HttpPost]
        [Route("pricingbooks")]
        public void Post([FromBody]PricingBookDTO newPriceBookDTO)
        {

            _priceLogic.AddNewListProduct(newPriceBookDTO);
        }

        // PUT: api/Student/12345
        [HttpPut]
        [Route("pricingbooks/{id}")]
        public PricingBookDTO Put([FromBody]PricingBookDTO pricingBookToUpdate, string id) // id=Code:12345
        {
            Console.WriteLine("from swagger=>" + pricingBookToUpdate.Id+"-"+pricingBookToUpdate.Name+"-"+pricingBookToUpdate.Description+"-"+pricingBookToUpdate.Status+"-"+pricingBookToUpdate.ProductPrices);
            return _priceLogic.UpdateListProduct(pricingBookToUpdate, id);
        }

        [HttpDelete]
        [Route("pricingbooks/{id}")]
        public void Delete(int id) // CI:65008816
        {
            _priceLogic.DeleteListProduct(id);

        }

    }
}
