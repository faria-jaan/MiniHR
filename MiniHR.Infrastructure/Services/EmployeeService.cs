using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;

namespace MiniHR.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _db;


        public EmployeeService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await _db.QueryAsync<EmployeeDto>("Employees_Get", commandType: CommandType.StoredProcedure);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<EmployeeDto>("Employees_GetById", new { EmployeeID = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(EmployeeDto dto, string performedBy)
        {
            await _db.ExecuteAsync("Employees_Create", new
            {
                dto.FullName,
                dto.Email,
                dto.Phone,
                dto.DateOfJoining,
                dto.DepartmentID,
                dto.IsActive,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(EmployeeDto dto, string performedBy)
        {
            await _db.ExecuteAsync("Employees_Update", new
            {
                dto.EmployeeID,
                dto.FullName,
                dto.Email,
                dto.Phone,
                dto.DateOfJoining,
                dto.DepartmentID,
                dto.IsActive,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }

        //public async Task DeleteAsync(int id, string performedBy)
        //{
        //    await _db.ExecuteAsync("Employees_Delete", new { EmployeeID = id, PerformedBy = performedBy }, commandType: CommandType.StoredProcedure);
        //}
        public async Task DeleteAsync(int employeeId, string performedBy)
        {
            try
            {
                await _db.ExecuteAsync("Employees_Delete", new
                {
                    EmployeeID = employeeId,
                    PerformedBy = performedBy
                }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {                
                throw new Exception(ex.Message);
            }
        }


        public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(int employeeId)
        {
            return await _db.QueryFirstOrDefaultAsync<LeaveBalanceDto>("GetEmployeeLeaveBalance", new { EmployeeID = employeeId }, commandType: CommandType.StoredProcedure);
        }
    }
}
