using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
using System.Threading.Tasks;

namespace MiniHR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalanceService _service;

        public LeaveBalanceController(ILeaveBalanceService service)
        {
            _service = service;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(int employeeId)
        {
            var result = await _service.GetByEmployeeIdAsync(employeeId);
            return Ok(result); //result != null ? Ok(result) : NotFound();
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(int employeeId, [FromBody] LeaveBalanceDto dto)
        {
            dto.EmployeeID = employeeId;
            await _service.UpdateAsync(dto, "Admin");
            return Ok("Leave balance updated");
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> ApplyLeave(int employeeId, [FromBody] LeaveBalanceDto dto)
        {
            dto.EmployeeID = employeeId;
            await _service.ApplyLeaveAsync(dto, "Admin");
            return Ok("Leave balance updated");
        }
        
    }
}
