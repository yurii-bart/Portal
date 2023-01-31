using Microsoft.AspNetCore.Mvc;
using Portal.Data.Models;
using Portal.Services.ModelServices.Invoices;
using Portal.Services.Reports;

namespace Portal.Controllers
{
    [Route("api/v1/invoices")]
    public class InvoiceApiController : BaseCrudApiController<Invoice>
    {
        private readonly ReportService _reportService;
        public InvoiceApiController(InvoiceModelService modelService, ReportService reportService) : base(modelService)
        {
            _reportService = reportService;
        }

        [HttpGet("{id:length(24)}/report")]
        public async Task<IActionResult> Report(string id, ReportType type)
        {
            var report = await _reportService.GenerateReport(id, type);
            return Content(report);
        }
    }
}
