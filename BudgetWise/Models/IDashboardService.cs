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
}