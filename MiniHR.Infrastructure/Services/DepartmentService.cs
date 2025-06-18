using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
namespace MiniHR.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDbConnection _db;

        public DepartmentService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _db.QueryAsync<DepartmentDto>("Department_Get", commandType: CommandType.StoredProcedure);
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<DepartmentDto>("Department_GetById", new { DepartmentID = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task CreateAsync(DepartmentDto dto, string performedBy)
        {
            await _db.ExecuteAsync("Departments_Create", new
            {
                dto.DepartmentName,
                dto.HeadOfDepartment,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(DepartmentDto dto, string performedBy)
        {
            await _db.ExecuteAsync("Departments_Update", new
            {
                dto.DepartmentID,
                dto.DepartmentName,
                dto.HeadOfDepartment,
                PerformedBy = performedBy
            }, commandType: CommandType.StoredProcedure);
        }

        //public async Task DeleteAsync(int id, string performedBy)
        //{
        //    await _db.ExecuteAsync("Departments_Delete", new { DepartmentID = id, PerformedBy = performedBy }, commandType: CommandType.StoredProcedure);
        //}

        public async Task DeleteAsync(int id, string performedBy)
        {
            try
            {
                await _db.ExecuteAsync("Departments_Delete", new
                {
                    DepartmentID = id,
                    PerformedBy = performedBy
                }, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                // Re-throw with message for controller to capture
                throw new Exception(ex.Message);
            }
        }

    }
}