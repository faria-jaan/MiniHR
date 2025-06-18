using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MiniHR.Application.DTOs;
using MiniHR.Application.Interfaces;
namespace MiniHR.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDbConnection _db;
        public DashboardService(IDbConnection db) { _db = db; }
        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            try
            {
                return await _db.QueryFirstAsync<DashboardSummaryDto>("sp_GetDashboardSummary", commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
