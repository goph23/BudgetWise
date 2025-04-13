using BudgetWise.Areas.Identity.Data;
using BudgetWise.Models;
using BudgetWise.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWise.Controllers
{
    // No Authorize attribute needed here - it's a publicly accessible controller
    public class DemoDashboardController : BaseDashboardController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DemoDashboardController(
            ILogger<DemoDashboardController> logger, 
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
            : base(logger)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Demo()
        {
            ViewData["isDashboard"] = null;
            ViewData["PageTitle"] = "Demonstration - Register/Login to Visualize your Finances";
            
            // Explicitly set isAuthenticated based on user status
            ViewData["isAuthenticated"] = User is not null && User.Identity is not null && User.Identity.IsAuthenticated;

            IDashboardService dashboardService = new DemoDashboardService();
            await PopulateDashboardData(dashboardService);
            
            return View("Demo");
        }
    }
}