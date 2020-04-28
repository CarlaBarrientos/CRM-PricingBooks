﻿using Microsoft.AspNetCore.Mvc;
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
        public void Delete(int id) // CI:65008816
        {
            _priceLogic.DeleteListProduct(id);

        }

    }
}
