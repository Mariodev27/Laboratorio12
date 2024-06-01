namespace Laboratorio12.Models
{
    public class Detail
    {

        public int DetailId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }

        //Product
        public Product Product { get; set; }
        public int ProductId { get; set; }

        //Invoice
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
