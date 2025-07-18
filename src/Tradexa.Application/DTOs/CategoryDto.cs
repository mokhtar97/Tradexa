using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
    }
}
