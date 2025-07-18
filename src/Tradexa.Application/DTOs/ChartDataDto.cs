using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class ChartDataDto
    {
        public List<string> Labels { get; set; }
        public List<decimal> Values { get; set; }
        public string ChartType { get; set; } // bar, pie, etc.
    }
}
