using Laboratorio12.Models;
using Laboratorio12.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsControllerV2 : ControllerBase
    {
        private readonly DemoContext demoContext;

        public DetailsControllerV2(DemoContext context)
        {
            demoContext = context;
        }

        [HttpPost]
        public List<Detail> InsertDetailsListInvoice(int idInvoice, List<DetailV1> details)
        {
            Invoice invoice = demoContext.Invoices.Where(x => x.InvoiceId == idInvoice).FirstOrDefault();

            List<Detail> detailsList = new List<Detail>();

            foreach (var detail in details)
            {
                Detail newDetail = new Detail
                {
                    InvoiceId = idInvoice,
                    Invoice = invoice,
                    Price = detail.Price,
                    Amount = detail.Amount,
                    SubTotal = detail.SubTotal
                };

                demoContext.Details.Add(newDetail);
                demoContext.SaveChanges();

                detailsList.Add(newDetail);
            }

            return detailsList;
        }
    }
}