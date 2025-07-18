using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class ReportFilterDto
    {
        public string EntityType { get; set; } // e.g. "Products", "Invoices", "Users"
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Language { get; set; } = "en";
    }
}
