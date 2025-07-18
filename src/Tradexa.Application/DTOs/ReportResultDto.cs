using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class ReportResultDto
    {
        public string Title { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
    }
}
