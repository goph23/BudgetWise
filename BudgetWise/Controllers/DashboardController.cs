using BudgetWise.Areas.Identity.Data;
using BudgetWise.Models;
using BudgetWise.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWise.Controllers
{
    [Authorize]
    public class DashboardController : BaseDashboardController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            ILogger<DashboardController> logger, 
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager) 
            : base(logger)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["isDashboard"] = true;
            
            string userId = _userManager.GetUserId(User) ?? string.Empty;
            IDashboardService dashboardService = new UserDashboardService(_context, _userManager, userId);
            
            await PopulateDashboardData(dashboardService);
            
            return View();
        }
    }
}