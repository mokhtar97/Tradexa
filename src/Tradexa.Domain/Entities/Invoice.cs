using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public decimal TotalAmount => Items.Sum(i => i.Total);
        public decimal TaxAmount { get; set; }
        public string? QRCodeBase64 { get; set; }
    }

}
