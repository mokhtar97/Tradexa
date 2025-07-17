using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Domain.Entities
{
    public class ProductProviderPrice
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;

        public DateTime EffectiveDate { get; set; }
        public decimal Price { get; set; }
    }
}
