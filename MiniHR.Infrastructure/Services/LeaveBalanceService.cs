using Dapper;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
using System.Data;
using System.Threading.Tasks;

public class LeaveBalanceService : ILeaveBalanceService
{
    private readonly IDbConnection _db;

    public LeaveBalanceService(IDbConnection db)
    {
        _db = db;
    }

    public async Task<LeaveBalanceDto> GetByEmployeeIdAsync(int employeeId)
    {
        return await _db.QueryFirstOrDefaultAsync<LeaveBalanceDto>(
            "LeaveBalance_GetByEmployeeId", new { EmployeeID = employeeId }, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(LeaveBalanceDto dto, string performedBy)
    {
        await _db.ExecuteAsync("sp_UpdateLeaveBalance", new
        {
            dto.EmployeeID,
            dto.AnnualLeave,
            dto.SickLeave,
            dto.UnpaidLeave,
            PerformedBy = performedBy
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task ApplyLeaveAsync(LeaveBalanceDto dto, string performedBy)
    {
        await _db.ExecuteAsync("LeaveRequest_ApplyLeave", new
        {
            dto.EmployeeID,
            dto.AnnualLeave,
            dto.SickLeave,
            dto.UnpaidLeave,
            PerformedBy = performedBy
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId)
    {
        var result = await _db.QuerySingleOrDefaultAsync<LeaveBalanceDto>(
            "GetEmployeeLeaveBalance",
            new { EmployeeID = employeeId },
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
}
// end of MiniHR.Infrastructure.Services
