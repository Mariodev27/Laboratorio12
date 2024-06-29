using Laboratorio12.Models;
using Laboratorio12.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly DemoContext demoContext;

        public InvoicesController(DemoContext context)
        {
            demoContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            if (demoContext.Invoices == null)
            {
                return NotFound();
            }
            return await demoContext.Invoices.Include(x => x.Customer).ToListAsync();
        }

        [HttpGet("{name}")]
        public ActionResult<List<Invoice>> GetInvoiceByName(string name)
        {
            var invoices = demoContext.Invoices
                .Include(x => x.Customer)
                .Where(x => x.Customer.FirstName.Contains(name))
                .OrderByDescending(x => x.Customer.FirstName)
                .ThenByDescending(x => x.InvoiceNumber)
                .ToList();

            if (invoices == null || !invoices.Any())
            {
                return NotFound();
            }

            return invoices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await demoContext.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoiceData)
        {
            if (id != invoiceData.InvoiceId)
            {
                return BadRequest();
            }

            demoContext.Entry(invoiceData).State = EntityState.Modified;

            try
            {
                await demoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Invoice>> InsertInvoice(Invoice invoice)
        {
            if (demoContext.Invoices == null)
            {
                return Problem("Entity set 'DemoContext.Invoices' is null.");
            }
            demoContext.Invoices.Add(invoice);
            await demoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, invoice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await demoContext.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            demoContext.Invoices.Remove(invoice);
            await demoContext.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return demoContext.Invoices.Any(e => e.InvoiceId == id);
        }
    }
}