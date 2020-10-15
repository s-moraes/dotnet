using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(int id,
                                                        [FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(categories);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<List<Category>>> Post([FromBody]Category model,
                                                            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                context.Categories.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);

            } catch (Exception e) {
                return BadRequest(new { message = "Internal Error, category not created"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<List<Category>>> Put(int id, [FromBody]Category model,
                                                                    [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new {message = "Categoria não encontrada"});

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);


            } catch (DbUpdateConcurrencyException e) {
                return BadRequest(new { message = "Internal Error, category already updated"});
            } catch (Exception e) {
                return BadRequest(new { message = "Internal Error, category not updated"});
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<List<Category>>> Delete(int id,
                                                            [FromServices] DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound (new {message = "Category not found"});

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new {message = "Category removed with success"});

            } catch {
                return BadRequest (new {message = "Internal Error when returning category"});
            }
        }
    }
}
