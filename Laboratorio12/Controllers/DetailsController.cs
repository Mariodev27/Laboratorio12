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
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly DemoContext _context;

        public DetailsController(DemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detail>>> GetDetails()
        {
            if (_context.Details == null)
            {
                return NotFound();
            }
            return await _context.Details.ToListAsync();
        }

        [HttpGet("getByInvoiceDate/{dateInvoice}")]
        public List<Detail> GetByInvoiceDate(DateTime dateInvoice)
        {
            var details = _context.Details
                .Include(x => x.Invoice)
                .Include(x => x.Product)
                .Where(x => x.Invoice.Date.Date == dateInvoice.Date)
                .OrderBy(x => x.Invoice.Date)
                .OrderBy(x => x.Product)
                .ToList();

            return details;
        }

        [HttpGet("getByInvoiceNumber/{invoiceNumber}")]
        public List<Detail> GetByInvoiceNumber(string invoiceNumber)
        {
            var details = _context.Details
                .Include(x => x.Invoice)
                .Include(x => x.Invoice.Customer)
                .Where(x => x.Invoice.InvoiceNumber.Contains(invoiceNumber))
                .OrderByDescending(x => x.Invoice.Customer.FirstName)
                .OrderByDescending(x => x.Invoice.InvoiceNumber)
                .ToList();

            return details;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Detail>> GetDetail(int id)
        {
            if (_context.Details == null)
            {
                return NotFound();
            }
            var detail = await _context.Details.FindAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            return detail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetail(int id, Detail detail)
        {
            if (id != detail.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailExists(id))
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
        public async Task<ActionResult<Detail>> PostDetail(Detail detail)
        {
            if (_context.Details == null)
            {
                return Problem("Entity set 'DemoContext.Details'  is null.");
            }
            _context.Details.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetail", new { id = detail.DetailId }, detail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            if (_context.Details == null)
            {
                return NotFound();
            }
            var detail = await _context.Details.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetailExists(int id)
        {
            return (_context.Details?.Any(e => e.DetailId == id)).GetValueOrDefault();
        }
    }
}