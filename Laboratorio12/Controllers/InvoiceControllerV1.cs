using Laboratorio12.Models;
using Laboratorio12.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceControllerV1 : ControllerBase
    {
        private readonly DemoContext demoContext;

        public InvoiceControllerV1(DemoContext context)
        {
            demoContext = context;
        }

        [HttpPost]
        public ActionResult<Invoice> InsertInvoice(InvoiceV1 invoiceData)
        {
            Invoice invoice = new Invoice
            {
                CustomerId = invoiceData.IdCustomer,
                Date = invoiceData.Date,
                InvoiceNumber = invoiceData.InvoiceNumber,
                Total = invoiceData.Total
            };

            demoContext.Invoices.Add(invoice);
            demoContext.SaveChanges();
            return Ok(invoice);
        }

        [HttpPost]
        public ActionResult<List<Invoice>> InsertListInvoiceByClient(int idCustomer, List<InvoiceV2> invoicesData)
        {
            var customer = demoContext.Customers.FirstOrDefault(x => x.CustomerId == idCustomer);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            List<Invoice> invoiceList = new List<Invoice>();

            foreach (var invoiceData in invoicesData)
            {
                Invoice newInvoice = new Invoice
                {
                    InvoiceNumber = invoiceData.InvoiceNumber,
                    Total = invoiceData.Total,
                    Date = invoiceData.Date,
                    CustomerId = idCustomer
                };

                demoContext.Invoices.Add(newInvoice);
                invoiceList.Add(newInvoice);
            }

            demoContext.SaveChanges();

            return invoiceList;
        }
    }
}