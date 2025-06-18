using MiniHR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Application.Interfaces
{
   public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task CreateAsync(EmployeeDto dto, string performedBy);
        Task UpdateAsync(EmployeeDto dto, string performedBy);
        Task DeleteAsync(int id, string performedBy);
        Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId);
    }
}
