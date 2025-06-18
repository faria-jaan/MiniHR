using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MiniHR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                var result = await _dashboardService.GetSummaryAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Show exact reason
            }
        }
    }
}
