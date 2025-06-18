using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MiniHR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result); /*result != null ? Ok(result) : NotFound();*/
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDto dto)
        {
            await _service.CreateAsync(dto, "Admin");
            return Ok("Department created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto dto)
        {
            dto.DepartmentID = id;
            await _service.UpdateAsync(dto, "Admin");
            return Ok("Department updated");
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteAsync(id, "Admin");
        //    return Ok("Department deleted");
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
    }
}
