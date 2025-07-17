using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Provider> Providers { get; set; } = new List<Provider>();
        public ICollection<ProductProviderPrice> ProviderPrices { get; set; } = new List<ProductProviderPrice>();

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
