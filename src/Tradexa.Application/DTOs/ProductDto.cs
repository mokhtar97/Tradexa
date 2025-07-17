using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; } = default!;
        public string ArabicName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; }
        public string CategoryEnglishName { get; set; }
        public string CategoryId { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }
        public List<string> ProviderNames { get; set; } = new();
    }

    // ProductCreateDto.cs
    public class ProductCreateDto
    {
        public string EnglishName { get; set; } = default!;
        public string ArabicName { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

        public List<Guid> ProviderIds { get; set; } = new();
    }

    // ProductUpdateDto.cs
    public class ProductUpdateDto : ProductCreateDto
    {
        public Guid Id { get; set; }
    }



    public class ProviderDto
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; } = string.Empty;
        public string ArabicName { get; set; } = string.Empty;
        public string? ContactInfo { get; set; }
    }

    public class PriceHistoryDto
    {
        public DateTime EffectiveDate { get; set; }
        public decimal Price { get; set; }
        public Guid ProviderId { get; set; }
        public string ProviderEnglishName { get; set; } = string.Empty;
        public string ProviderArabicName { get; set; } = string.Empty;
    }
}
