using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Module1.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Module1.Data;
using System.Linq;
using Module1.Services;

namespace Module1.Controllers
{
    [Route("api/Products")]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private IProducts _productsRepository;

        public ProductsController(IProducts productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get ()
        {
            return _productsRepository.GetProducts();
        }

        /*[HttpGet]
        public IEnumerable<Product> Get (string searchProduct)
        {
            var products = _productsDbContext.Products.Where(p => p.ProductName.Contains(searchProduct));
            return products;
        }*/

        // Paging
        // /api/Products?pageNumber=2&pageSize=3
        /*[HttpGet]
        public IEnumerable<Product> Get(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 99;

            // LINQ
            var product = from p in _productsContextDb.Products.OrderBy(a => a.ProductId) select p;
            var items = product.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).ToList();

            return items;
        }*/

        // Sorting
        /*[HttpGet]
        public IEnumerable<Product> Get(string sortPrice)
        {
            IQueryable<Product> products;
            switch (sortPrice) {
                case "desc":
                    Console.WriteLine("desc");
                    products = _productsContextDb.Products.OrderByDescending(p => p.ProductPrice);
                    break;

                case "asc":
                    Console.WriteLine("asc");
                    products = _productsContextDb.Products.OrderBy(p => p.ProductPrice);
                    break;
                
                default:
                    products = _productsContextDb.Products;
                    break;
            }

            return products;
        }*/

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = _productsRepository.GetProduct(id);
            if (product == null)
                return NotFound("No record found...");

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _productsRepository.AddProduct(product);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put (int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.ProductId)
                return BadRequest(ModelState);

            try {
                _productsRepository.UpdateProduct(product);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

            return Ok("Product Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            _productsRepository.DeleteProduct(id);

            return Ok("Product Removed");
        }
    }
}