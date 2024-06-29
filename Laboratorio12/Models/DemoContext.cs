using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Laboratorio12.Models;

namespace Laboratorio12.Models
{
    public class DemoContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Detail> Details { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-N5FLK4B\\MARIO27; " +
                 "Initial Catalog=Lab14;User ID=mario;Password=1234; Integrated Security=True; trustservercertificate=True");
        }


        public DbSet<Laboratorio12.Models.Customer> Customer { get; set; } = default!;
    }
}
