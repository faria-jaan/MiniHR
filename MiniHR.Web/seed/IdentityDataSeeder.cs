using Microsoft.AspNetCore.Identity;
using MiniHR.Application.DTOs;
using MiniHR.Web.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiniHR.Web.Seed
{
    public static class IdentityDataSeeder
    {
     
        public static async Task SeedAdminUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Seed Roles
            string[] roles = { "Admin", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Seed Admin User
            string adminEmail = "admin@hr.com";
            string adminPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        //private const string ApiBase = "https://localhost:44331/api/";
        private const string ApiBase = "https://localhost:44331/api/employee";

        public static async Task SeedRolesAndEmployees(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpClientFactory httpFactory)
        {
            
            if (!await roleManager.RoleExistsAsync("Employee"))
                await roleManager.CreateAsync(new IdentityRole("Employee"));

            var _httpClient = httpFactory.CreateClient();
            //var client = httpFactory.CreateClient();
            //client.BaseAddress = new Uri(ApiBase);
            //var response = await client.GetAsync("employees");
            var response = await _httpClient.GetAsync(ApiBase);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Unable to fetch employees: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(json);

           
            foreach (var emp in employees)
            {
                if (await userManager.FindByEmailAsync(emp.Email) != null)
                    continue;

                var user = new IdentityUser
                {
                    UserName = emp.Email,
                    Email = emp.Email,
                    EmailConfirmed = true
                };

                // You can generate or configure stronger passwords here
                var result = await userManager.CreateAsync(user, "123@Emp");
                if (!result.Succeeded)
                {
                    // log or handle errors
                    continue;
                }

                await userManager.AddToRoleAsync(user, "Employee");
            }
        }
    }



}
