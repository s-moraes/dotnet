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

    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var users = await context
                            .User
                            .AsNoTracking()
                            .ToListAsync();
            return users;
        }


        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.Role = "employee";

                context.User.Add(model);
                await context.SaveChangesAsync();

                model.Password = ""; // erase passwd when returning
                return Ok(model);
            } catch {
                return BadRequest(new {message = "Internal Error, user not created"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<User>> Put(
            int id,
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.Id)
                return NotFound(new {message = "User not found"});


            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            } catch {
                return BadRequest(new {message = "Internal Error, user not changed"});
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await context.User
                                .AsNoTracking()
                                .Where(x => x.Username == model.Username && x.Password == model.Password)
                                .FirstOrDefaultAsync();

            if (user == null)
                return BadRequest(new {message = "Invalid user or passwd"});

            var token = TokenService.GenerateToken(user);
            user.Password = ""; // erase passwd when returning
            
            return new {
                user = user,
                token = token
            };
        }
        
    }

}