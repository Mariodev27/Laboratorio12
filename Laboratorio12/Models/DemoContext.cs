using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Laboratorio12.Models;

namespace Laboratorio12.Models
{
    public class DemoContext: DbContext
    {
        public DbSet<Detail> Details{ get; set; }
        public DbSet<Product> Products{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAB1504-03\\SQLEXPRESS; " +
                 "Initial Catalog=FacturaDB;User ID=mario;Password=1234; Integrated Security=True; trustservercertificate=True");
        }


        public DbSet<Laboratorio12.Models.Customer> Customer { get; set; } = default!;
    }
}
