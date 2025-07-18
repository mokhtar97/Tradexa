using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service) => _service = service;

   // [HttpPost("filter")]
    //public async Task<IActionResult> Filter(ReportFilterDto dto) => Ok(await _service.GetReportAsync(dto));
}
