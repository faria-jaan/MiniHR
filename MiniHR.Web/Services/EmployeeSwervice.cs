// 📁 MiniHR.Web/Services/EmployeeService.cs
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MiniHR.Application.DTOs;

namespace MiniHR.Web.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"] + "/api/employee";
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDto>>(json);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeDto>(json);
        }

        public async Task CreateAsync(EmployeeDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(EmployeeDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{dto.EmployeeID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}/leave-balance");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LeaveBalanceDto>(json);
        }
    }
}
