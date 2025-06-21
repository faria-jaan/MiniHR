using Microsoft.AspNetCore.Mvc;
using MiniHR.Application.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:44331/api/department";

        public DepartmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
                return View(data);
            }
            return View(new List<DepartmentDto>());
        }

        public IActionResult Create()
        {
            return View(new DepartmentDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Department created successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating department.");
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DepartmentDto>(json);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Department updated successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating department.");
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Department deleted successfully!";
                return RedirectToAction("Index");
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            string errorMessage = "Error deleting employee.";
            try
            {
                dynamic errorObj = JsonConvert.DeserializeObject(errorContent);
                errorMessage = errorObj?.message ?? errorMessage;
            }
            catch
            {
                errorMessage = errorContent;
            }
            TempData["ErrorMessage"] = errorContent;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<DepartmentDto>(json);
            return View(dto);
        }
    }
}
