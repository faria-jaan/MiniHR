using MiniHR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Application.Interfaces
{
    public interface ILeaveBalanceService
    {
        Task<LeaveBalanceDto> GetByEmployeeIdAsync(int employeeId);
        Task UpdateAsync(LeaveBalanceDto dto, string performedBy);
         Task ApplyLeaveAsync(LeaveBalanceDto dto, string performedBy);
        Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId);
    }
}
