using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MiniHR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestController(ILeaveRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeaveRequestDto dto)
        {
            //await _service.CreateAsync(dto, "Admin");
            //return Ok("Leave request created");
            try
            {
                await _service.CreateAsync(dto, "Admin");
                return Ok("Leave request created"); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Show exact reason
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LeaveRequestDto dto)
        {
            try
            {
                dto.LeaveID = id;
                await _service.UpdateAsync(dto, "Admin");
                return Ok("Leave request updated");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Show exact reason
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id, "Admin");
                return Ok("Leave request deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Show exact reason
            }
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveAsync(id, "System"); // replace "System" with logged-in user
            return Ok();
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            await _service.RejectAsync(id, "System");
            return Ok();
        }

        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {
            var list = await _service.GetByEmployeeAsync(employeeId);
            return Ok(list);
        }
    }
}

