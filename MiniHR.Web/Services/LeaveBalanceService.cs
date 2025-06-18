using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MiniHR.Application.DTOs;

namespace MiniHR.Web.Services
{
    public class LeaveBalanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public LeaveBalanceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"].TrimEnd('/') + "/api/leavebalance";
        }

        public async Task<LeaveBalanceDto> GetByEmployeeIdAsync(int employeeId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{employeeId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LeaveBalanceDto>(json);
        }

        public async Task UpdateAsync(LeaveBalanceDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{dto.EmployeeID}", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
