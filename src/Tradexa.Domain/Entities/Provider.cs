using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;

        public string? ContactInfo { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<ProductProviderPrice> ProductPrices { get; set; } = new List<ProductProviderPrice>();
    }

}
