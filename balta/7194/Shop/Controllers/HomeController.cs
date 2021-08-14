using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{

    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var employee1 = new User {Id = 1, Username = "user1", Password = "user1", Role = "employee"};
            var employee2 = new User {Id = 2, Username = "user2", Password = "user2", Role = "employee"};
            var manager = new User {Id = 3, Username = "manager1", Password = "manager1", Role = "manager"};
            var category1 = new Category {Id = 1, Title = "Category 01"};
            var category2 = new Category {Id = 2, Title = "Category 02"};
            var product1 = new Product {Id = 1, Category = category1, Title="Prod01", Description="Desc", Price=2, CategoryId=category1.Id};
            var product2 = new Product {Id = 2, Category = category1, Title="Prod02", Description="Desc", Price=8, CategoryId=category1.Id};
            var product3 = new Product {Id = 3, Category = category2, Title="Prod03", Description="Desc", Price=20, CategoryId=category2.Id};
            var product4 = new Product {Id = 4, Category = category2, Title="Prod04", Description="Desc", Price=92, CategoryId=category2.Id};
            
            context.User.Add(employee1);
            context.User.Add(employee2);
            context.User.Add(manager);

            context.Categories.Add(category1);
            context.Categories.Add(category2);

            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }

}