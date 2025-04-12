using BudgetWise.Areas.Identity.Data;
using BudgetWise.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BudgetWise.Services
{
    public class UserDashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _userId;

        public UserDashboardService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, string userId)
        {
            _context = context;
            _userManager = userManager;
            _userId = userId;
        }

        public async Task<string> GetTotalIncome()
        {
            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.UserId == _userId)
                .OrderBy(y => y.Date)
                .ToListAsync();
            
            DateTime? earliestDate = selectedTransactions.FirstOrDefault()?.Date;
            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }
            
            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;
            int totalIncome = selectedTransactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Income")
                .Sum(j => j.Amount);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            return String.Format(culture, "{0:C0}", totalIncome);
        }

        public async Task<string> GetTotalExpense()
        {
            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.UserId == _userId)
                .OrderBy(y => y.Date)
                .ToListAsync();
            
            DateTime? earliestDate = selectedTransactions.FirstOrDefault()?.Date;
            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }
            
            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;
            int totalExpense = selectedTransactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Expense")
                .Sum(j => j.Amount);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            return String.Format(culture, "{0:C0}", totalExpense);
        }

        public async Task<string> GetBalance()
        {
            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.UserId == _userId)
                .OrderBy(y => y.Date)
                .ToListAsync();
            
            DateTime? earliestDate = selectedTransactions.FirstOrDefault()?.Date;
            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }
            
            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;
            int totalIncome = selectedTransactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Income")
                .Sum(j => j.Amount);
            
            int totalExpense = selectedTransactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Expense")
                .Sum(j => j.Amount);
            
            int balance = totalIncome - totalExpense;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            
            return String.Format(culture, "{0:C0}", balance);
        }

        public async Task<List<object>> GetTreemapData()
        {
            // Define the start and end date for the past week
            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today;

            // Retrieve the transactions for the user within the specified date range
            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate && y.UserId == _userId)
                .ToListAsync();

            // Create a CultureInfo object for formatting currency values
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            // Group the transactions by category, calculate the sum of amounts,
            // and format the amount as currency using the specified culture
            var treemapData = selectedTransactions
                .Where(i => i.Category?.Type == "Expense")
                .GroupBy(j => j.Category?.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category?.Icon + " " + k.First().Category?.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = String.Format(culture, "{0:C0}", k.Sum(j => j.Amount))
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            // Cast the list to a list of objects and return, or an empty list if null
            return treemapData.Cast<object>().ToList() ?? new List<object>();
        }

        public async Task<List<BarChartData>> GetBarChartData()
        {
            DateTime startDate = DateTime.Today.AddDays(-6); // Last 7 days including today
            DateTime endDate = DateTime.Today; // Include today

            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate && y.UserId == _userId)
                .ToListAsync();

            var barChartData = selectedTransactions
                .GroupBy(t => t.Date.Date) // Group by date only
                .Select(g => new BarChartData
                {
                    date = g.Key,
                    day = g.Key.ToString("MMM dd"),
                    income = g.Where(t => t.Category?.Type == "Income").Sum(t => t.Amount),
                    expense = g.Where(t => t.Category?.Type == "Expense").Sum(t => t.Amount)
                })
                .OrderBy(d => d.date)
                .ToList();

            return barChartData ?? new List<BarChartData>();
        }

        public async Task<List<object>> GetStackedColumnChartData()
        {
            DateTime startDate = DateTime.Today.AddDays(-29);
            DateTime endDate = DateTime.Today; // Include today

            var selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(t => t.Date.Date >= startDate && t.Date.Date <= endDate && t.UserId == _userId)
                .ToListAsync();

            var stackedColumnChartData = selectedTransactions
                .GroupBy(t => new { Date = t.Date.Date, Type = t.Category?.Type ?? "Unknown" })
                .Select(g => new
                {
                    Date = g.Key.Date,
                    Type = g.Key.Type,
                    Amount = g.Sum(t => t.Amount)
                })
                .GroupBy(x => x.Date)
                .Select(y => new
                {
                    Date = y.Key,
                    Income = y.Where(z => z.Type == "Income").Sum(z => z.Amount),
                    Expense = y.Where(z => z.Type == "Expense").Sum(z => z.Amount)
                })
                .OrderBy(x => x.Date)
                .ToList();

            return stackedColumnChartData.Cast<object>().ToList();
        }

        public async Task<List<BubbleChartData>> GetBubbleChartData()
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate && y.UserId == _userId)
                .ToListAsync();

            DateTime? earliestTransactionDate = selectedTransactions
                .OrderBy(t => t.Date)
                .Select(t => t.Date.Date)
                .FirstOrDefault();

            if (earliestTransactionDate.HasValue)
            {
                startDate = earliestTransactionDate.Value;
            }

            var bubbleChartData = selectedTransactions
                .Where(t => t.Date.Date >= startDate)
                .GroupBy(t => new { t.Category?.Title, t.Category?.Type })
                .Select(g => new BubbleChartData
                {
                    category = g.Key.Title ?? "Unknown",
                    type = g.Key.Type ?? "Unknown",
                    amount = g.Sum(t => t.Amount),
                    size = g.Count()
                })
                .OrderBy(d => d.size)
                .ToList();

            return bubbleChartData ?? new List<BubbleChartData>();
        }

        public async Task<List<MonthlyTrendData>> GetMonthlyTrendChartData()
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate && y.UserId == _userId)
                .ToListAsync();

            DateTime? earliestTransactionDate = selectedTransactions
                .OrderBy(t => t.Date)
                .Select(t => t.Date.Date)
                .FirstOrDefault();

            if (earliestTransactionDate.HasValue && earliestTransactionDate.Value < startDate)
            {
                startDate = new DateTime(earliestTransactionDate.Value.Year, earliestTransactionDate.Value.Month, 1);
            }

            List<MonthlyTrendData> monthlyTrendChartData = new List<MonthlyTrendData>();

            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                int totalIncome = selectedTransactions
                    .Where(i => i.Category?.Type == "Income" && i.Date.Year == date.Year && i.Date.Month == date.Month)
                    .Sum(j => j.Amount);

                int totalExpense = selectedTransactions
                    .Where(i => i.Category?.Type == "Expense" && i.Date.Year == date.Year && i.Date.Month == date.Month)
                    .Sum(j => j.Amount);

                int balance = totalIncome - totalExpense;

                monthlyTrendChartData.Add(new MonthlyTrendData
                {
                    Month = date.ToString("MMM yyyy"),
                    Income = totalIncome,
                    Expense = totalExpense,
                    Balance = balance
                });
            }

            return monthlyTrendChartData;
        }

        public async Task<List<object>> GetStackedAreaChartData()
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate && y.UserId == _userId)
                .ToListAsync();

            DateTime? earliestTransactionDate = selectedTransactions
                .OrderBy(t => t.Date)
                .Select(t => t.Date.Date)
                .FirstOrDefault();

            if (earliestTransactionDate.HasValue && earliestTransactionDate.Value < startDate)
            {
                startDate = new DateTime(earliestTransactionDate.Value.Year, earliestTransactionDate.Value.Month, 1);
            }

            var stackedAreaChartData = selectedTransactions
                .Where(t => t.Date.Date >= startDate && t.Date.Date <= endDate)
                .GroupBy(t => new { Month = t.Date.ToString("MMM yyyy"), Type = t.Category?.Type ?? "Unknown" })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Type = g.Key.Type,
                    Amount = g.Sum(t => t.Amount)
                })
                .GroupBy(x => x.Month)
                .Select(y => new
                {
                    Month = y.Key,
                    Income = y.Where(z => z.Type == "Income").Sum(z => z.Amount),
                    Expense = y.Where(z => z.Type == "Expense").Sum(z => z.Amount),
                    FormattedIncome = y.Where(z => z.Type == "Income").Sum(z => z.Amount).ToString("C0"),
                    FormattedExpense = y.Where(z => z.Type == "Expense").Sum(z => z.Amount).ToString("C0")
                })
                .OrderBy(x => DateTime.ParseExact(x.Month, "MMM yyyy", CultureInfo.InvariantCulture))
                .ToList();

            return stackedAreaChartData.Cast<object>().ToList();
        }
    }
}