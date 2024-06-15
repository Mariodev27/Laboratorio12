using Laboratorio12.Models;
using Laboratorio12.Models.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Laboratorio12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerControllerV1 : ControllerBase
    {
        private readonly DemoContext demoContext;

        [HttpPost]
        public Customer insertCustomer(CustomerV1 customerData)
        {
            Customer customer = new Customer
            {
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                DocumentNumber = customerData.DocumentNumber,
                Active = 1
            };

            demoContext.Customers.Add(customer);
            demoContext.SaveChanges();
            return customer;

        }

        [HttpDelete]
        public Customer deleteCustomer(int id)
        {
            Customer customer = demoContext.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            customer.Active = 0;

            demoContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            demoContext.SaveChanges();

            return customer;
        }

        [HttpPut]
        public Customer updateCustomer(CustomerV2 customerData)
        {
            Customer customer = demoContext.Customers.Where(x => x.CustomerId == customerData.Id).FirstOrDefault();

            customer.DocumentNumber = customerData.DocumentNumber;

            demoContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            demoContext.SaveChanges();

            return customer;

        }
    }
}
