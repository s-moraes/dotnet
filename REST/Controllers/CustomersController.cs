using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Module1.Models;

namespace Module1.Controllers
{
    [Route("api/Customers")]
    [Produces("application/json")]
    public class CustomersController : Controller
    {
        static List<Customer> _customer = new List<Customer>() {
            new Customer(){Id = 0, Name = "AA", Email = "AA@gmail.com", Phone = "111111111"},
            new Customer(){Id = 1, Name = "BB", Email = "BB@gmail.com", Phone = "222222222"},
            new Customer(){Id = 2, Name = "CC", Email = "CC@gmail.com", Phone = "333333333"}
        };

        [HttpGet]
        public IEnumerable<Customer> Get() {
            return _customer;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer) {

            if (ModelState.IsValid)
            {
                _customer.Add(customer);
                return Ok();
            }

            return BadRequest(ModelState);            
        }
    }
}