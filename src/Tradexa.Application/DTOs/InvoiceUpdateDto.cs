using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class InvoiceUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerTaxId { get; set; }

        public string SellerName { get; set; }
        public string SellerTaxId { get; set; }

        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public string Notes { get; set; }

        [Required]
        public List<InvoiceProductUpdateDto> Products { get; set; } = new();
    }

    public class InvoiceProductUpdateDto
    {
        public int? Id { get; set; } // Optional: if stored separately

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductArabicName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal Total => UnitPrice * Quantity;
    }
}
