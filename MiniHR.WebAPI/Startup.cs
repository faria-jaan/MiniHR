using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniHR.Application.Interfaces;
using MiniHR.Infrastructure.Services;
using System.Data;
using System.Data.SqlClient;

namespace MiniHR.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            
            services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

           
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILeaveRequestService, LeaveRequestService>();
            services.AddTransient<ILeaveBalanceService, LeaveBalanceService>();
            services.AddTransient<IDashboardService, DashboardService>();

           
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}"); // 👈 Redirect root to Login
            });
        }
    }
}
