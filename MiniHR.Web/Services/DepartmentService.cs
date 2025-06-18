using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MiniHR.Application.DTOs;

namespace MiniHR.Web.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public DepartmentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"].TrimEnd('/') + "/api/department";
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<DepartmentDto>>(json);
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DepartmentDto>(json);
        }

        public async Task CreateAsync(DepartmentDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(DepartmentDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{dto.DepartmentID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}// 📁 MiniHR.Web/Services/EmployeeService.cs
