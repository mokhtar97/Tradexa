using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;
using Tradexa.Infrastructure.Persistence;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;
using QuestPDF.Helpers;

namespace Tradexa.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportResultDto> GenerateReportAsync(ReportFilterDto filter)
        {
            var result = new ReportResultDto
            {
                Title = GetLocalizedTitle(filter.EntityType, filter.Language),
                Data = await GetEntityDataAsync(filter)
            };

            return result;
        }

        public async Task<byte[]> ExportReportToExcelAsync(ReportFilterDto filter)
        {
            var report = await GenerateReportAsync(filter);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Report");

            if (report.Data.Any())
            {
                var headers = report.Data.First().Keys.ToList();
                for (int i = 0; i < headers.Count; i++)
                    worksheet.Cells[1, i + 1].Value = headers[i];

                for (int r = 0; r < report.Data.Count; r++)
                {
                    var row = report.Data[r];
                    for (int c = 0; c < headers.Count; c++)
                    {
                        worksheet.Cells[r + 2, c + 1].Value = row[headers[c]];
                    }
                }
            }

            return await package.GetAsByteArrayAsync();
        }

        public async Task<byte[]> ExportReportToPdfAsync(ReportFilterDto filter)
        {
            var report = await GenerateReportAsync(filter);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text(report.Title).Bold().FontSize(20);
                    page.Content().Table(table =>
                    {
                        var headers = report.Data.FirstOrDefault()?.Keys.ToList();
                        if (headers == null) return;

                        table.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < headers.Count; i++)
                                columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            foreach (var col in headers)
                                header.Cell().Element(CellStyle).Text(col).Bold();
                        });

                        foreach (var row in report.Data)
                        {
                            foreach (var col in headers)
                                table.Cell().Element(CellStyle).Text(row[col]?.ToString() ?? "-");
                        }

                        static IContainer CellStyle(IContainer container) =>
                            container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text($"Generated on {DateTime.Now}");
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }

        public async Task<ChartDataDto> GetChartDataAsync(ReportFilterDto filter)
        {
            var chart = new ChartDataDto { Labels = new(), Values = new() };

            if (filter.EntityType == "Invoices")
            {
                var data = await _context.Invoices
                    .Where(i => i.InvoiceDate >= filter.FromDate && i.InvoiceDate <= filter.ToDate)
                    .GroupBy(i => i.InvoiceDate.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Total = g.Sum(i => i.TotalAmount)
                    }).ToListAsync();

                foreach (var entry in data)
                {
                    chart.Labels.Add(entry.Date.ToShortDateString());
                    chart.Values.Add(entry.Total);
                }

                chart.ChartType = "bar";
            }

            // Additional logic for other entities...
            return chart;
        }

        private async Task<List<Dictionary<string, object>>> GetEntityDataAsync(ReportFilterDto filter)
        {
            if (filter.EntityType == "Products")
            {
                return await _context.Products
                    .Where(p => p.CreatedAt >= filter.FromDate && p.CreatedAt <= filter.ToDate)
                    .Select(p => new Dictionary<string, object>
                    {
                        { filter.Language == "ar" ? "اسم المنتج" : "Product Name", p.ArabicName },
                        { filter.Language == "ar" ? "السعر" : "Price", p.Price },
                        { filter.Language == "ar" ? "المخزون" : "Stock", p.Stock }
                    }).ToListAsync();
            }

            if (filter.EntityType == "Invoices")
            {
                return await _context.Invoices
                    .Where(i => i.InvoiceDate >= filter.FromDate && i.InvoiceDate <= filter.ToDate)
                    .Select(i => new Dictionary<string, object>
                    {
                        { filter.Language == "ar" ? "رقم الفاتورة" : "Invoice Number", i.Id },
                        { filter.Language == "ar" ? "التاريخ" : "Date", i.InvoiceDate },
                        { filter.Language == "ar" ? "الإجمالي" : "Total", i.TotalAmount }
                    }).ToListAsync();
            }

            // Extend for Users, Categories, Providers, etc.

            return new List<Dictionary<string, object>>();
        }

        private string GetLocalizedTitle(string entityType, string lang)
        {
            return (entityType, lang) switch
            {
                ("Products", "ar") => "تقرير المنتجات",
                ("Invoices", "ar") => "تقرير الفواتير",
                ("Users", "ar") => "تقرير المستخدمين",
                ("Products", _) => "Product Report",
                ("Invoices", _) => "Invoice Report",
                ("Users", _) => "User Report",
                _ => "Report"
            };
        }
    }
}
