using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniHR.Application.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        private const string ApiUrl = "https://localhost:44331/api/employee";

        
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Admin"))
            {
                var response = await _httpClient.GetAsync(ApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<EmployeeDto>>(json);
                    return View(data);
                }

                return View(new List<EmployeeDto>());
            }
            else { return RedirectToAction("Dashboard", "Home"); }
        }

        public async Task<IActionResult> Create()
        {
           
            var resp = await _httpClient.GetAsync("https://localhost:44331/api/department");
            List<DepartmentDto> depts;
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                depts = JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
            }
            else
            {
                depts = new List<DepartmentDto>();
            }

            ViewBag.Departments = depts;
         
            return View(new EmployeeDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto dto)
        {
           
            var deptResponse = await _httpClient.GetAsync("https://localhost:44331/api/department");

            if (deptResponse.IsSuccessStatusCode)
            {
                var json = await deptResponse.Content.ReadAsStringAsync();
                var departments = JsonConvert.DeserializeObject<List<DepartmentDto>>(json);
                ViewBag.DepartmentSelectList = new SelectList(departments, "DepartmentID", "DepartmentName");
            }


            if (!ModelState.IsValid)
            {
                
                var errors = ModelState
                    .Where(kv => kv.Value.Errors.Count > 0)
                    .ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                ViewBag.DebugModelErrors = errors;  
                return View(dto);
            }



            var jsonContent = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiUrl, content);

            
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Error creating employee.");
            return View(dto);
        }

       public async Task<IActionResult> Edit(int id)
        {
            // 1) Load the employee data
            var resp = await _httpClient.GetAsync($"{ApiUrl}/{id}");
            if (!resp.IsSuccessStatusCode) return NotFound();
            var empJson = await resp.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<EmployeeDto>(empJson);

            // 2) Load departments
            var respDept = await _httpClient.GetAsync("https://localhost:44331/api/department");
            var deptJson = await respDept.Content.ReadAsStringAsync();
            var depts = JsonConvert.DeserializeObject<List<DepartmentDto>>(deptJson);
            ViewBag.Departments = new SelectList(depts, "DepartmentID", "DepartmentName", dto.DepartmentID);
            dto.Departments = depts;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeDto dto)
        {
            var respDept = await _httpClient.GetAsync("https://localhost:44331/api/department");
            var deptJson = await respDept.Content.ReadAsStringAsync();
            var depts = JsonConvert.DeserializeObject<List<DepartmentDto>>(deptJson);
            dto.Departments = depts;
            ViewBag.Departments = new SelectList(depts, "DepartmentID", "DepartmentName", dto.DepartmentID);


            if (!ModelState.IsValid)
                return View(dto);

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync($"{ApiUrl}/{dto.EmployeeID}", content);

            if (resp.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error updating employee.");
            return View(dto);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, EmployeeDto dto)
        //{

        //    var respDept = await _httpClient.GetAsync("https://localhost:44331/api/department");
        //    if (respDept.IsSuccessStatusCode)
        //    {
        //        var deptJson = await respDept.Content.ReadAsStringAsync();
        //        var depts = JsonConvert.DeserializeObject<List<DepartmentDto>>(deptJson);
        //        ViewBag.Departments = new SelectList(depts, "DepartmentID", "DepartmentName", dto.DepartmentID);
        //    }
        //    else
        //    {
        //        ViewBag.Departments = new SelectList(new List<DepartmentDto>());
        //    }

        //    if (!ModelState.IsValid)
        //        return View(dto);

        //    var json = JsonConvert.SerializeObject(dto);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var resp = await _httpClient.PutAsync($"{ApiUrl}/{id}", content);

        //    if (resp.IsSuccessStatusCode)
        //    {
        //        TempData["SuccessMessage"] = "Employee updated successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ModelState.AddModelError("", "Error updating employee.");
        //    return View(dto);
        //}



        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee deleted successfully!";
                return RedirectToAction(nameof(Index));
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

            TempData["ErrorMessage"] = errorMessage;
            return RedirectToAction(nameof(Index));
        }
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest("Could not delete employee.");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<EmployeeDto>(json);
                return View(dto);
            }

            return NotFound();
        }


    }
}
