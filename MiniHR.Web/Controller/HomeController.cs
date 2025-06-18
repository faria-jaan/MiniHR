using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace MiniHR.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDbConnection _db;

        public HomeController(UserManager<IdentityUser> userManager, IDbConnection db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.IsAdmin = roles.Contains("Admin");
            ViewBag.UserName = user.UserName;

            if (ViewBag.IsAdmin)
            {
                var summary = await _db.QueryFirstOrDefaultAsync<DashboardSummaryDto>(
                    "sp_GetDashboardSummary", commandType: CommandType.StoredProcedure);
                ViewBag.Summary = summary;
            }

            return View();
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public class DashboardSummaryDto
    {
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int PendingLeaveRequests { get; set; }
    }
}
