using BudgetWise.Models;
using BudgetWise.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWise.Controllers
{
    // Base controller without authorization
    public abstract class BaseDashboardController : Controller
    {
        protected readonly ILogger _logger;

        public BaseDashboardController(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task PopulateDashboardData(IDashboardService dashboardService)
        {
            ViewBag.TotalIncome = await dashboardService.GetTotalIncome();
            ViewBag.TotalExpense = await dashboardService.GetTotalExpense();
            ViewBag.Balance = await dashboardService.GetBalance();
            ViewBag.TreemapData = await dashboardService.GetTreemapData() ?? new List<object>();
            ViewBag.BarChartData = await dashboardService.GetBarChartData() ?? new List<BarChartData>();
            ViewBag.MonthlyTrendChartData = await dashboardService.GetMonthlyTrendChartData() ?? new List<MonthlyTrendData>();
            ViewBag.BubbleChartData = await dashboardService.GetBubbleChartData() ?? new List<BubbleChartData>();
            ViewBag.StackedColumnChartData = await dashboardService.GetStackedColumnChartData() ?? new List<object>();
            ViewBag.StackedAreaChartData = await dashboardService.GetStackedAreaChartData() ?? new List<object>();
            
            // New budget metrics
            ViewBag.DailyBudgetMetrics = await dashboardService.GetDailyBudgetMetrics();
            ViewBag.MonthlyBudgetMetrics = await dashboardService.GetMonthlyBudgetMetrics();
            ViewBag.YearlyBudgetMetrics = await dashboardService.GetYearlyBudgetMetrics();
            ViewBag.TopExpenseCategories = await dashboardService.GetTopExpenseCategories();
            ViewBag.SavingsRate = await dashboardService.GetSavingsRate();
            ViewBag.ExpenseBreakdown = await dashboardService.GetExpenseBreakdown();
            ViewBag.RecentTransactions = await dashboardService.GetRecentTransactions();
        }
    }
}