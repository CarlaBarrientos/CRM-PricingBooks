using CRM_PricingBooks.Controllers.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_PricingBooks.BusinessLogic;

namespace CRM_PricingBooks.Controllers
{
    [Route("api/pricingBooks")]
    [ApiController]

    public class PricingBookController : ControllerBase
    {   
        private readonly IPriceLogic _priceLogic;

        public PricingBookController(IPriceLogic pricelogic)
        {
            _priceLogic = pricelogic;
        }
        
        [HttpGet]
        public IEnumerable<PricingBookDTO> GetAll()
        {
            return _priceLogic.GetPricingBooks();
        }

        [HttpPost]
        [Route("Productlists")]
        public void Post([FromBody]PricingBookDTO newPriceBookDTO)
        {

            _priceLogic.AddNewListProduct(newPriceBookDTO);
        }

        // PUT: api/Student/12345
        [HttpPut]
        [Route("products/{id}")]
        public void Put([FromBody]PricingBookDTO productToUpdate, int id) // id=Code:12345
        {
            _priceLogic.UpdateListProduct(productToUpdate,id);
        }

        [HttpDelete]
        [Route("products/{id}")]
        public void Delete(int id) // CI:65008816
        {
            _priceLogic.DeleteListProduct(id);

        }

    }
}
