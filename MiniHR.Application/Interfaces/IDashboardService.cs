using MiniHR.Application.DTOs;
using System.Threading.Tasks;

namespace MiniHR.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}