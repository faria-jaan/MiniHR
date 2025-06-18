using MiniHR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllAsync();
        Task<LeaveRequestDto> GetByIdAsync(int id);
        Task CreateAsync(LeaveRequestDto dto, string performedBy);
        Task UpdateAsync(LeaveRequestDto dto, string performedBy);
        Task DeleteAsync(int id, string performedBy);
        Task ApproveAsync(int leaveId, string performedBy);
        Task RejectAsync(int leaveId, string performedBy);
        Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(int employeeId);
    }
}
