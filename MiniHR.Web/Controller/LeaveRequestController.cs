using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly HttpClient _httpClient;

        public LeaveRequestController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        private const string ApiBase = "https://localhost:44331/api/";

        // GET: /LeaveRequest
        public async Task<IActionResult> Index(string search)
        {
            var list = new List<LeaveRequestDto>();
            if (User.IsInRole("Admin"))
            {
                var response = await _httpClient.GetAsync(ApiBase + "leaverequest");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<LeaveRequestDto>>(json);
                }
            }
            else
            {
                var email = User.Identity.Name;
                var empResp = await _httpClient.GetAsync(ApiBase + "employee");
                if (!empResp.IsSuccessStatusCode)
                    return StatusCode((int)empResp.StatusCode);

                var empJson = await empResp.Content.ReadAsStringAsync();
                var employees = JsonConvert
                    .DeserializeObject<List<EmployeeDto>>(empJson);
                var me = employees
                    .FirstOrDefault(e =>
                        e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (me == null) return Forbid();
                var lrResp = await _httpClient
                    .GetAsync(ApiBase + $"leaverequest/by-employee/{me.EmployeeID}");
                if (!lrResp.IsSuccessStatusCode)
                    return StatusCode((int)lrResp.StatusCode);

                var lrJson = await lrResp.Content.ReadAsStringAsync();
                list = JsonConvert
                    .DeserializeObject<List<LeaveRequestDto>>(lrJson);
            }
            //    var employeeIdClaim = User.FindFirst("EmployeeID")?.Value;
            //    if (!int.TryParse(employeeIdClaim, out var employeeId))
            //        return Forbid();

            //    var response = await _httpClient.GetAsync(ApiBase + $"leaverequest/by-employee/{employeeId}");
            //    response.EnsureSuccessStatusCode();
            //    list = JsonConvert.DeserializeObject<List<LeaveRequestDto>>(await response.Content.ReadAsStringAsync());
            //}



            if (!string.IsNullOrWhiteSpace(search))
            {
                ViewBag.Search = search;
                list = list.FindAll(r => r.LeaveType != null && r.LeaveType.ToLower().Contains(search.ToLower()));
            }

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, string actionType)
        {
            if (actionType == "Approve")
            {
                var response  =  await _httpClient.PostAsync(ApiBase + $"leaverequest/{id}/approve", null);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"Leave #{id} approved.";
                    return RedirectToAction(nameof(Index));
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = "Error in aproving request.";
                try
                {
                    dynamic errorObj = JsonConvert.DeserializeObject(errorContent);
                    errorMessage = errorObj?.message ?? errorMessage;
                }
                catch
                {
                    errorMessage = errorContent;
                }
                //ViewBag.DebugModelErrors = errorMessage;
                TempData["ErrorMessage"] = errorMessage;
                TempData["Message"] = $"Leave #{id} approved.";
                return RedirectToAction(nameof(Index));
            }
            else if (actionType == "Reject")
            {
                var response = await _httpClient.PostAsync(ApiBase + $"leaverequest/{id}/reject", null);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"Leave #{id} Rejected.";
                    return RedirectToAction(nameof(Index));
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = "Error in rejecting request.";
                try
                {
                    dynamic errorObj = JsonConvert.DeserializeObject(errorContent);
                    errorMessage = errorObj?.message ?? errorMessage;
                }
                catch
                {
                    errorMessage = errorContent;
                }
                TempData["Message"] = $"Leave #{id} rejected.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /LeaveRequest/Create
        public async Task<IActionResult> Create()
        {
            LeaveRequestDto lr = new LeaveRequestDto();
            //var userId = User.Identity.Name;
            //var employeeId = 5;        
            var email = User.Identity.Name;
            var empResp = await _httpClient.GetAsync(ApiBase + "employee");
            if (!empResp.IsSuccessStatusCode)
                return StatusCode((int)empResp.StatusCode);

            var empJson = await empResp.Content.ReadAsStringAsync();
            var employees = JsonConvert
                .DeserializeObject<List<EmployeeDto>>(empJson);
            var me = employees
                .FirstOrDefault(e =>
                    e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (me == null) return Forbid();
            var lrResp = await _httpClient
                .GetAsync(ApiBase + $"leaverequest/by-employee/{me.EmployeeID}");
            if (!lrResp.IsSuccessStatusCode)
                return StatusCode((int)lrResp.StatusCode);

            var balanceResponse = await _httpClient.GetAsync(ApiBase + $"employee/{me.EmployeeID}/leave-balance");

            if (balanceResponse.IsSuccessStatusCode)
            {
                var json = await balanceResponse.Content.ReadAsStringAsync();
                var balance = JsonConvert.DeserializeObject<LeaveBalanceDto>(json);
                ViewBag.LeaveBalance = balance;
            }
            lr.StartDate = DateTime.Today;
            lr.EndDate = DateTime.Today;
            return View(lr);
        }

        // POST: /LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestDto dto)
        {
            var email = User.Identity.Name;
            var empResp = await _httpClient.GetAsync(ApiBase + "employee");
            if (!empResp.IsSuccessStatusCode)
                return StatusCode((int)empResp.StatusCode);

            var empJson = await empResp.Content.ReadAsStringAsync();
            var employees = JsonConvert
                .DeserializeObject<List<EmployeeDto>>(empJson);
            var me = employees
                .FirstOrDefault(e =>
                    e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            dto.EmployeeID = me.EmployeeID;
            var lrResp = await _httpClient.GetAsync(ApiBase + $"leaverequest/by-employee/{me.EmployeeID}");
            if (!lrResp.IsSuccessStatusCode)
                return StatusCode((int)lrResp.StatusCode);

            var balanceResponse = await _httpClient.GetAsync(ApiBase + $"employee/{me.EmployeeID}/leave-balance");
            if (balanceResponse.IsSuccessStatusCode)
            {
                var balacejson = await balanceResponse.Content.ReadAsStringAsync();
                var balance = JsonConvert.DeserializeObject<LeaveBalanceDto>(balacejson);
                ViewBag.LeaveBalance = balance;
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
            dto.Status = "Pending";
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiBase + "leaverequest", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            var errorContent = await response.Content.ReadAsStringAsync();
            string errorMessage = "Error for Applying Leave.";
            try
            {
                dynamic errorObj = JsonConvert.DeserializeObject(errorContent);
                errorMessage = errorObj?.message ?? errorMessage;
            }
            catch
            {
                errorMessage = errorContent;
            }
            //ViewBag.DebugModelErrors = errorMessage;
            TempData["ErrorMessage"] = errorMessage;
            return View(dto);
        }





    }
}
