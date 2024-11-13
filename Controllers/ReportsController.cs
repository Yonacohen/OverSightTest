using Microsoft.AspNetCore.Mvc;
using OverSightTest.Interfaces;

namespace OverSightTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetByDate([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var response = _reportService.GetCouponsByDate(fromDate, toDate);
            return response.ToActionResult();
        }

        [HttpGet("user")]
        public IActionResult GetByCreatedUser()
        {
            var response = _reportService.GetCouponsByUser(User.Identity.Name);
            return response.ToActionResult();
        }

        [HttpPost]
        public IActionResult ExportToExcel()
        {
            var response = _reportService.ExportToExcel();
            return response.ToActionResult();
        }
    }

}
