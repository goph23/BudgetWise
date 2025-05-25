using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWise.Models
{
    public interface IDashboardService
    {
        Task<string> GetTotalIncome();
        Task<string> GetTotalExpense();
        Task<string> GetBalance();
        Task<List<object>> GetTreemapData();
        Task<List<BarChartData>> GetBarChartData();
        Task<List<object>> GetStackedColumnChartData();
        Task<List<BubbleChartData>> GetBubbleChartData();
        Task<List<MonthlyTrendData>> GetMonthlyTrendChartData();
        Task<List<object>> GetStackedAreaChartData();
        
        // New budget metrics
        Task<BudgetMetrics> GetDailyBudgetMetrics();
        Task<BudgetMetrics> GetMonthlyBudgetMetrics();
        Task<BudgetMetrics> GetYearlyBudgetMetrics();
        Task<List<CategoryExpense>> GetTopExpenseCategories();
        Task<decimal> GetSavingsRate();
        Task<List<ExpenseBreakdown>> GetExpenseBreakdown();
        Task<List<RecentTransaction>> GetRecentTransactions();
    }

    public class MonthlyTrendData
    {
        public string Month { get; set; }
        public int Income { get; set; }
        public int Expense { get; set; }
        public int Balance { get; set; }
    }

    public class BarChartData
    {
        public DateTime date { get; set; }
        public string day;
        public int income;
        public int expense;
    }

    public class BubbleChartData
    {
        public string category;
        public string type;
        public int amount;
        public int size;
    }
    
    public class BudgetMetrics
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
        public decimal AverageExpense { get; set; }
        public decimal AverageIncome { get; set; }
        public string Period { get; set; }
        public int TransactionCount { get; set; }
    }
    
    public class CategoryExpense
    {
        public string CategoryName { get; set; }
        public string Icon { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public int TransactionCount { get; set; }
    }
    
    public class ExpenseBreakdown
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Icon { get; set; }
    }
    
    public class RecentTransaction
    {
        public DateTime Date { get; set; }
        public string CategoryName { get; set; }
        public string Icon { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
    }
}