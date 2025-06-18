using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiniHR.Web.Data
{
    // This context is used for ASP.NET Core Identity tables
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // If you want to use EF Core for your entities as well, you can uncomment and add:
        // public DbSet<Department> Departments { get; set; }
        // public DbSet<Employee> Employees { get; set; }
        // public DbSet<LeaveRequest> LeaveRequests { get; set; }
        // public DbSet<LeaveBalance> LeaveBalances { get; set; }
        // public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
