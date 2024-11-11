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
        public IActionResult GetByCreatedUser([FromQuery] Guid userId)
        {
            var response = _reportService.GetCouponsByUser(userId);
            return response.ToActionResult();
        }
    }

}
