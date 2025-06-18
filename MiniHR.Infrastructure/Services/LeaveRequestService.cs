using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly IDbConnection _db;

    public LeaveRequestService(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllAsync()
    {
        return await _db.QueryAsync<LeaveRequestDto>("LeaveRequest_Get", commandType: CommandType.StoredProcedure);
    }

    public async Task<LeaveRequestDto> GetByIdAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<LeaveRequestDto>("LeaveRequest_GetById", new { LeaveID = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task CreateAsync(LeaveRequestDto dto, string performedBy)
    {
        try
        {
            await _db.ExecuteAsync("LeaveRequest_Create", new
            {
                dto.EmployeeID,
                dto.LeaveType,
                dto.StartDate,
                dto.EndDate,
                dto.Status,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateAsync(LeaveRequestDto dto, string performedBy)
    {
        try
        {
            await _db.ExecuteAsync("LeaveRequest_Update", new
            {
                dto.LeaveID,
                dto.EmployeeID,
                dto.LeaveType,
                dto.StartDate,
                dto.EndDate,
                dto.Status,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteAsync(int id, string performedBy)
    {
        try
        {
            await _db.ExecuteAsync("LeaveRequest_Delete", new { LeaveID = id, PerformedBy = performedBy }, commandType: CommandType.StoredProcedure);
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task ApproveAsync(int leaveId, string performedBy)
    {
        await _db.ExecuteAsync("LeaveRequest_Approve", new
        {
            LeaveID = leaveId,
            PerformedBy = performedBy
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task RejectAsync(int leaveId, string performedBy)
    {
        await _db.ExecuteAsync("LeaveRequest_Reject", new
        {
            LeaveID = leaveId,
            PerformedBy = performedBy
        }, commandType: CommandType.StoredProcedure);
    }
    public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(int employeeId)
    {
        return await _db.QueryAsync<LeaveRequestDto>(
            "GetLeaveRequestsByEmployee",
            new { EmployeeID = employeeId },
            commandType: CommandType.StoredProcedure);
    }

}
