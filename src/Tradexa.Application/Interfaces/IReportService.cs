using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradexa.Application.DTOs;

namespace Tradexa.Application.Interfaces
{
    public interface IReportService
    {
        Task<ReportResultDto> GenerateReportAsync(ReportFilterDto filter);

        Task<byte[]> ExportReportToPdfAsync(ReportFilterDto filter);

        Task<byte[]> ExportReportToExcelAsync(ReportFilterDto filter);

        Task<ChartDataDto> GetChartDataAsync(ReportFilterDto filter);
    }
}
