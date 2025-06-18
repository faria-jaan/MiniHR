using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MiniHR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
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
            return Ok(result); //result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            await _service.CreateAsync(dto, "Admin");
            return Ok("Employee created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
        {
            dto.EmployeeID = id;
            await _service.UpdateAsync(dto, "Admin");
            return Ok("Employee updated");
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id, "Admin");
        //    return Ok("Employee deleted");
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id, "Admin");
                return NoContent(); // 204 if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Show exact reason
            }
        }

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