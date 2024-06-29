using Laboratorio12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DemoContext demoContext;

        public ProductsController(DemoContext context)
        {
            demoContext = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (demoContext.Products == null)
            {
                return NotFound();
            }
            return await demoContext.Products
                .Where(product => product.Active == 1)
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (demoContext.Products == null)
            {
                return NotFound();
            }
            var product = await demoContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            demoContext.Entry(product).State = EntityState.Modified;

            try
            {
                await demoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (demoContext.Products == null)
            {
                return Problem("Entity set 'DemoContext.Products' is null.");
            }
            demoContext.Products.Add(product);
            await demoContext.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (demoContext.Products == null)
            {
                return NotFound();
            }
            var product = await demoContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Active = 0;

            demoContext.Entry(product).State = EntityState.Modified;
            await demoContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (demoContext.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}