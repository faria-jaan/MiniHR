using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using MiniHR.Web.Services;
using System.Threading.Tasks;

namespace MiniHR.Web.Controllers
{
    public class LeaveBalanceController : Controller
    {
        private readonly LeaveBalanceService _service;

        public LeaveBalanceController(LeaveBalanceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int employeeId)
        {
            var dto = await _service.GetByEmployeeIdAsync(employeeId);
            return View(dto);
        }

        public async Task<IActionResult> Edit(int employeeId)
        {
            var dto = await _service.GetByEmployeeIdAsync(employeeId);
            return View(dto);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(LeaveBalanceDto dto)
        //{
        //    if (!ModelState.IsValid) return View(dto);
        //    await _service.UpdateAsync(dto);
        //    return RedirectToAction("Index", new { employeeId = dto.EmployeeID });
        //}


        [HttpGet("{id}/leave-balance")]
        public async Task<IActionResult> GetLeaveBalance(int id)
        {
            var balance = await _service.GetLeaveBalanceAsync(id);
            if (balance == null)
                return NotFound();

            return Ok(balance);
        }
    }
}
