using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratorio12.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }

        //Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
