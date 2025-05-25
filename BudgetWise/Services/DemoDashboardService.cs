using BudgetWise.Models;
using System.Globalization;

namespace BudgetWise.Services
{
    public class DemoDashboardService : IDashboardService
    {
        private readonly List<Category> _demoCategories;
        private readonly List<Transaction> _demoTransactions;

        public DemoDashboardService()
        {
            _demoCategories = GetDemoCategories();
            _demoTransactions = GenerateDemoTransactions(500, _demoCategories);
        }

        public Task<string> GetTotalIncome()
        {
            string result = CalculateTotalDemoIncome(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<string> GetTotalExpense()
        {
            string result = CalculateTotalDemoExpense(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<string> GetBalance()
        {
            string result = CalculateDemoBalance(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<object>> GetTreemapData()
        {
            List<object> result = GetDemoTreemapData(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<BarChartData>> GetBarChartData()
        {
            List<BarChartData> result = GetDemoBarChartData(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<object>> GetStackedColumnChartData()
        {
            List<object> result = GetDemoStackedColumnChartData(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<BubbleChartData>> GetBubbleChartData()
        {
            List<BubbleChartData> result = GetDemoBubbleChartData(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<MonthlyTrendData>> GetMonthlyTrendChartData()
        {
            List<MonthlyTrendData> result = GetDemoMonthlyTrendChartData(_demoTransactions);
            return Task.FromResult(result);
        }

        public Task<List<object>> GetStackedAreaChartData()
        {
            List<object> result = GetDemoStackedAreaChartData(_demoTransactions);
            return Task.FromResult(result);
        }

        #region Demo Data Generation

        private List<Category> GetDemoCategories()
        {
            return new List<Category>
            {
                new Category { CategoryId = 1, Title = "401(k)/IRA withdrawals", Icon = "💲", Type = "Income", UserId = "demo-user" ,  MinAmount = 500, MaxAmount = 5000 },
                new Category { CategoryId = 2, Title = "Auto insurance premiums", Icon = "🚗", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 300 },
                new Category { CategoryId = 3, Title = "Bonuses", Icon = "➕", Type = "Income", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 5000 },
                new Category { CategoryId = 4, Title = "Brokerage account contributions", Icon = "🏦", Type = "Expense", UserId = "demo-user" ,  MinAmount = 200, MaxAmount = 1000 },
                new Category { CategoryId = 5, Title = "Capital gains from investments", Icon = "💵", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 2000 },
                new Category { CategoryId = 6, Title = "Car fuel", Icon = "⛽", Type = "Expense", UserId = "demo-user" ,  MinAmount = 30, MaxAmount = 100 },
                new Category { CategoryId = 7, Title = "Car insurance", Icon = "🚙", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 200 },
                new Category { CategoryId = 8, Title = "Charitable donations", Icon = "🎁", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 200 },
                new Category { CategoryId = 9, Title = "Clothing", Icon = "👕", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 200 },
                new Category { CategoryId = 10, Title = "College savings fund", Icon = "🏫", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 11, Title = "Commission", Icon = "💸", Type = "Income", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 2000 },
                new Category { CategoryId = 12, Title = "Credit card payments", Icon = "💳", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 13, Title = "Dental care", Icon = "🦷", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 300 },
                new Category { CategoryId = 14, Title = "Dining out", Icon = "🍽️", Type = "Expense", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 100 },
                new Category { CategoryId = 15, Title = "Dividends from stocks/bonds", Icon = "📈", Type = "Income", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 500 },
                new Category { CategoryId = 16, Title = "Electricity", Icon = "💡", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 150 },
                new Category { CategoryId = 17, Title = "Emergency fund", Icon = "🚑", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 1000 },
                new Category { CategoryId = 18, Title = "Family gifts", Icon = "🎁", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 19, Title = "Freelancing/Consulting fees", Icon = "💼", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 20, Title = "Gas utilities", Icon = "💧", Type = "Expense", UserId = "demo-user" ,  MinAmount = 30, MaxAmount = 150 },
                new Category { CategoryId = 21, Title = "Gifts for family and friends", Icon = "🎁", Type = "Expense", UserId = "demo-user" ,  MinAmount = 200, MaxAmount = 500 },
                new Category { CategoryId = 22, Title = "Groceries", Icon = "🛒", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 200 },
                new Category { CategoryId = 23, Title = "Gym memberships", Icon = "🏋️", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 100 },
                new Category { CategoryId = 24, Title = "Haircuts/haircare", Icon = "💇", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 100 },
                new Category { CategoryId = 25, Title = "Health insurance premiums", Icon = "🏥", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 500 },
                new Category { CategoryId = 26, Title = "Hobbies", Icon = "🎨", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 200 },
                new Category { CategoryId = 27, Title = "Home insurance", Icon = "🏡", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 300 },
                new Category { CategoryId = 28, Title = "Home repairs and maintenance", Icon = "🔧", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 1000 },
                new Category { CategoryId = 29, Title = "Insurance settlements", Icon = "📃", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 300 },
                new Category { CategoryId = 30, Title = "Interest from savings accounts", Icon = "💰", Type = "Income", UserId = "demo-user" ,  MinAmount = 1, MaxAmount = 100 },
                new Category { CategoryId = 31, Title = "Internet/cable", Icon = "📡", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 150 },
                new Category { CategoryId = 32, Title = "Life insurance premiums", Icon = "🏥", Type = "Expense", UserId = "demo-user" ,  MinAmount = 20, MaxAmount = 100 },
                new Category { CategoryId = 33, Title = "Meal delivery services", Icon = "🍔", Type = "Expense", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 50 },
                new Category { CategoryId = 34, Title = "Medical copays", Icon = "🏥", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 300 },
                new Category { CategoryId = 35, Title = "Morning coffee", Icon = "☕", Type = "Expense", UserId = "demo-user" ,  MinAmount = 2, MaxAmount = 10 },
                new Category { CategoryId = 36, Title = "Mortgage/rent", Icon = "🏡", Type = "Expense", UserId = "demo-user" ,  MinAmount = 500, MaxAmount = 2000 },
                new Category { CategoryId = 37, Title = "Movie/concert tickets", Icon = "🎫", Type = "Expense", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 50 },
                new Category { CategoryId = 38, Title = "Overtime pay", Icon = "⏰", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 39, Title = "Parking fees", Icon = "🅿️", Type = "Expense", UserId = "demo-user" ,  MinAmount = 5, MaxAmount = 20 },
                new Category { CategoryId = 40, Title = "Personal loans", Icon = "🏦", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 41, Title = "Phone bill", Icon = "📞", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 150 },
                new Category { CategoryId = 42, Title = "Primary job salary", Icon = "💼", Type = "Income", UserId = "demo-user" ,  MinAmount = 500, MaxAmount = 2000 },
                new Category { CategoryId = 43, Title = "Profit from owned businesses", Icon = "🏢", Type = "Income", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 5000 },
                new Category { CategoryId = 44, Title = "Property taxes", Icon = "🏡", Type = "Expense", UserId = "demo-user" ,  MinAmount = 500, MaxAmount = 3000 },
                new Category { CategoryId = 45, Title = "Public transportation", Icon = "🚌", Type = "Expense", UserId = "demo-user" ,  MinAmount = 2, MaxAmount = 20 },
                new Category { CategoryId = 46, Title = "Real estate rental income", Icon = "🏢", Type = "Income", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 1000 },
                new Category { CategoryId = 47, Title = "Retirement contributions (401(k), IRA)", Icon = "💹", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 48, Title = "Savings account deposits", Icon = "🏦", Type = "Expense", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 49, Title = "School supplies", Icon = "✏️", Type = "Expense", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 100 },
                new Category { CategoryId = 50, Title = "School tuition", Icon = "🏫", Type = "Expense", UserId = "demo-user" ,  MinAmount = 500, MaxAmount = 2000 },
                new Category { CategoryId = 51, Title = "Side gigs", Icon = "💼", Type = "Income", UserId = "demo-user" ,  MinAmount = 50, MaxAmount = 500 },
                new Category { CategoryId = 52, Title = "Streaming subscriptions", Icon = "📺", Type = "Expense", UserId = "demo-user" ,  MinAmount = 10, MaxAmount = 50 },
                new Category { CategoryId = 53, Title = "Student loan payments", Icon = "🎓", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 500 },
                new Category { CategoryId = 54, Title = "Tax refunds", Icon = "💵", Type = "Income", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 2000 },
                new Category { CategoryId = 55, Title = "Travel/vacations", Icon = "✈️", Type = "Expense", UserId = "demo-user" ,  MinAmount = 100, MaxAmount = 2000 },
                new Category { CategoryId = 56, Title = "Water bill", Icon = "💧", Type = "Expense", UserId = "demo-user" , MinAmount = 20, MaxAmount = 100 },
            };
        }

        private List<Transaction> GenerateDemoTransactions(int count, List<Category> categories, int maxTransactionsPerDay = 10)
        {
            var random = new Random();
            var transactions = new List<Transaction>();
            var transactionCounts = new Dictionary<DateTime, int>();
            var incomeCategories = categories.Where(c => c.Type == "Income").ToList();
            var expenseCategories = categories.Where(c => c.Type == "Expense").ToList();
            int totalIncome = 0;
            int totalExpense = 0;
            int incomeCount = 0;
            var usedCategories = new HashSet<int>();
            var last7DaysCategories = new Dictionary<DateTime, HashSet<int>>();

            void AddTransaction(Category category, int amount, DateTime date)
            {
                var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
                var localDate = TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), localTimeZone);
                transactions.Add(new Transaction
                {
                    TransactionId = transactions.Count + 1,
                    CategoryId = category.CategoryId,
                    Category = category,
                    Amount = amount,
                    Note = $"Demo note {transactions.Count + 1}",
                    Date = localDate.Date,
                    UserId = "demo-user"
                });
                usedCategories.Add(category.CategoryId);
                if (!last7DaysCategories.ContainsKey(localDate.Date))
                {
                    last7DaysCategories[localDate.Date] = new HashSet<int>();
                }
                last7DaysCategories[localDate.Date].Add(category.CategoryId);
                if (!transactionCounts.ContainsKey(localDate.Date))
                {
                    transactionCounts[localDate.Date] = 0;
                }
                transactionCounts[localDate.Date]++;
                if (category.Type == "Income")
                {
                    totalIncome += amount;
                    incomeCount++;
                }
                else
                {
                    totalExpense += amount;
                }
            }

            DateTime startDate = DateTime.Today.AddDays(-365);
            DateTime endDate = DateTime.Today;
            int daysPassed = 0;

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                daysPassed++;
                var weekStart = date.AddDays(-(int)date.DayOfWeek);
                var categoriesThisWeek = new HashSet<int>();

                for (int i = 0; i < 7; i++)
                {
                    var currentDate = weekStart.AddDays(i);
                    if (last7DaysCategories.ContainsKey(currentDate))
                    {
                        categoriesThisWeek.UnionWith(last7DaysCategories[currentDate]);
                    }
                }

                int dailyIncome = 0;
                int dailyExpense = 0;
                int incomeTransactions = 0;
                int expenseTransactions = 0;

                while (transactionCounts.GetValueOrDefault(date, 0) < maxTransactionsPerDay &&
                       (incomeTransactions == 0 || expenseTransactions == 0 ||
                        random.Next(2) == 0 || categoriesThisWeek.Count < 10))
                {
                    Category category;
                    int amount;

                    if (incomeTransactions <= expenseTransactions && (expenseTransactions > 0 || random.Next(2) == 0))
                    {
                        // Generate income transaction
                        category = incomeCategories[random.Next(incomeCategories.Count)];
                        amount = random.Next(category.MinAmount, category.MaxAmount + 1);
                        dailyIncome += amount;
                        incomeTransactions++;
                    }
                    else
                    {
                        // Generate expense transaction
                        category = expenseCategories[random.Next(expenseCategories.Count)];
                        double timeRatio = 1.0 - (double)daysPassed / 366;
                        int maxExpenseAmount = Math.Max(Math.Min((int)(dailyIncome * 0.5), category.MaxAmount), category.MinAmount);
                        amount = random.Next(category.MinAmount, maxExpenseAmount + 1);
                        dailyExpense += amount;
                        expenseTransactions++;
                    }

                    if (!categoriesThisWeek.Contains(category.CategoryId))
                    {
                        AddTransaction(category, amount, date);
                        categoriesThisWeek.Add(category.CategoryId);
                    }
                }

                // Ensure daily expense is about half or less of daily income
                if (dailyExpense > dailyIncome * 0.5)
                {
                    int adjustment = (int)(dailyExpense - dailyIncome * 0.5);
                    var expenseTransactionsToday = transactions.Where(t => t.Category?.Type == "Expense" && t.Date.Date == date.Date).ToList();
                    while (adjustment > 0 && expenseTransactionsToday.Any())
                    {
                        var transaction = expenseTransactionsToday[random.Next(expenseTransactionsToday.Count)];
                        if (transaction != null && transaction.Category != null)
                        {
                            int reduceAmount = Math.Min(adjustment, transaction.Amount - transaction.Category.MinAmount);
                            transaction.Amount -= reduceAmount;
                            adjustment -= reduceAmount;
                            totalExpense -= reduceAmount;
                            if (transaction.Amount == transaction.Category.MinAmount)
                            {
                                expenseTransactionsToday.Remove(transaction);
                            }
                        }
                    }
                }
            }

            // Calculate average income
            int averageIncome = incomeCount > 0 ? totalIncome / incomeCount : 0;

            // Add final income transaction with average amount
            var finalIncomeCategory = incomeCategories[random.Next(incomeCategories.Count)];
            AddTransaction(finalIncomeCategory, averageIncome, DateTime.Today);

            return transactions;
        }

        private string CalculateTotalDemoIncome(List<Transaction> transactions)
        {
            DateTime? earliestDate = transactions.OrderBy(t => t.Date).FirstOrDefault()?.Date;

            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }

            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;

            int totalIncome = transactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Income")
                .Sum(j => j.Amount);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            return String.Format(culture, "{0:C0}", totalIncome);
        }

        private string CalculateTotalDemoExpense(List<Transaction> transactions)
        {
            DateTime? earliestDate = transactions.OrderBy(t => t.Date).FirstOrDefault()?.Date;

            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }

            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;

            int totalExpense = transactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Expense")
                .Sum(j => j.Amount);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            return String.Format(culture, "{0:C0}", totalExpense);
        }

        private string CalculateDemoBalance(List<Transaction> transactions)
        {
            DateTime? earliestDate = transactions.OrderBy(t => t.Date).FirstOrDefault()?.Date;

            if (earliestDate == null)
            {
                return 0.ToString("C0");
            }

            DateTime startDate = earliestDate.Value;
            DateTime endDate = DateTime.Today;

            int totalIncome = transactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Income")
                .Sum(j => j.Amount);

            int totalExpense = transactions
                .Where(i => i.Date.Date >= startDate && i.Date.Date <= endDate && i.Category?.Type == "Expense")
                .Sum(j => j.Amount);

            int balance = totalIncome - totalExpense;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            return String.Format(culture, "{0:C0}", balance);
        }

        private List<object> GetDemoTreemapData(List<Transaction> transactions)
        {
            // Define the start and end date for the past week
            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today;

            // Create a CultureInfo object for formatting currency values
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;

            // Group the transactions by category, calculate the sum of amounts,
            // and format the amount as currency using the specified culture
            var treemapData = transactions
                .Where(t => t.Date.Date >= startDate && t.Date.Date <= endDate && t.Category?.Type == "Expense")
                .GroupBy(j => j.Category?.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category?.Icon + " " + k.First().Category?.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = String.Format(culture, "{0:C0}", k.Sum(j => j.Amount))
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            // Cast the list to a list of objects and return
            return treemapData.Cast<object>().ToList();
        }

        private List<BarChartData> GetDemoBarChartData(List<Transaction> transactions)
        {
            // Define start date as 6 days ago from today, including today
            DateTime startDate = DateTime.Today.AddDays(-6);
            // Define end date as today
            DateTime endDate = DateTime.Today;

            // Filter and group transactions by date, then select the necessary data
            var barChartData = transactions
                .Where(y => y.Date.Date >= startDate && y.Date.Date <= endDate)
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

            // Return the bar chart data
            return barChartData;
        }

        private List<object> GetDemoStackedColumnChartData(List<Transaction> transactions)
        {
            DateTime startDate = DateTime.Today.AddDays(-29); // Last 30 days including today
            DateTime endDate = DateTime.Today; // Include today

            var stackedColumnChartData = transactions
                .Where(t => t.Date.Date >= startDate && t.Date.Date <= endDate)
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

        private List<BubbleChartData> GetDemoBubbleChartData(List<Transaction> transactions)
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            DateTime? earliestTransactionDate = transactions
                .OrderBy(t => t.Date)
                .Select(t => t.Date.Date)
                .FirstOrDefault();

            if (earliestTransactionDate.HasValue)
            {
                startDate = earliestTransactionDate.Value;
            }

            var bubbleChartData = transactions
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

            return bubbleChartData;
        }

        private List<MonthlyTrendData> GetDemoMonthlyTrendChartData(List<Transaction> transactions)
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            DateTime? earliestTransactionDate = transactions
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
                int totalIncome = transactions
                    .Where(i => i.Category?.Type == "Income" && i.Date.Year == date.Year && i.Date.Month == date.Month)
                    .Sum(j => j.Amount);

                int totalExpense = transactions
                    .Where(i => i.Category?.Type == "Expense" && i.Date.Year == date.Year && i.Date.Month == date.Month)
                    .Sum(j => j.Amount);

                int balance = totalIncome - totalExpense;

                monthlyTrendChartData.Add(new MonthlyTrendData
                {
                    Month = date.ToString("MMM yyyy", CultureInfo.InvariantCulture),
                    Income = totalIncome,
                    Expense = totalExpense,
                    Balance = balance
                });
            }

            return monthlyTrendChartData;
        }

        private List<object> GetDemoStackedAreaChartData(List<Transaction> transactions)
        {
            DateTime startDate = DateTime.Today.AddMonths(-11);
            DateTime endDate = DateTime.Today;

            DateTime? earliestTransactionDate = transactions
                .OrderBy(t => t.Date)
                .Select(t => t.Date.Date)
                .FirstOrDefault();

            if (earliestTransactionDate.HasValue && earliestTransactionDate.Value < startDate)
            {
                startDate = new DateTime(earliestTransactionDate.Value.Year, earliestTransactionDate.Value.Month, 1);
            }

            var stackedAreaChartData = transactions
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

        #endregion
        
        public Task<BudgetMetrics> GetDailyBudgetMetrics()
        {
            var today = DateTime.Today;
            var todayTransactions = _demoTransactions.Where(t => t.Date.Date == today).ToList();
            
            var income = todayTransactions.Where(t => t.Category?.Type == "Income").Sum(t => t.Amount);
            var expense = todayTransactions.Where(t => t.Category?.Type == "Expense").Sum(t => t.Amount);
            
            return Task.FromResult(new BudgetMetrics
            {
                Income = income,
                Expense = expense,
                Balance = income - expense,
                AverageExpense = expense,
                AverageIncome = income,
                Period = "Today",
                TransactionCount = todayTransactions.Count
            });
        }
        
        public Task<BudgetMetrics> GetMonthlyBudgetMetrics()
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            
            var monthTransactions = _demoTransactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();
                
            var income = monthTransactions.Where(t => t.Category?.Type == "Income").Sum(t => t.Amount);
            var expense = monthTransactions.Where(t => t.Category?.Type == "Expense").Sum(t => t.Amount);
            var daysPassed = (DateTime.Today - startDate).Days + 1;
            
            return Task.FromResult(new BudgetMetrics
            {
                Income = income,
                Expense = expense,
                Balance = income - expense,
                AverageExpense = daysPassed > 0 ? expense / daysPassed : 0,
                AverageIncome = daysPassed > 0 ? income / daysPassed : 0,
                Period = startDate.ToString("MMMM yyyy"),
                TransactionCount = monthTransactions.Count
            });
        }
        
        public Task<BudgetMetrics> GetYearlyBudgetMetrics()
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1);
            var endDate = new DateTime(DateTime.Today.Year, 12, 31);
            
            var yearTransactions = _demoTransactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();
                
            var income = yearTransactions.Where(t => t.Category?.Type == "Income").Sum(t => t.Amount);
            var expense = yearTransactions.Where(t => t.Category?.Type == "Expense").Sum(t => t.Amount);
            var daysPassed = (DateTime.Today - startDate).Days + 1;
            
            return Task.FromResult(new BudgetMetrics
            {
                Income = income,
                Expense = expense,
                Balance = income - expense,
                AverageExpense = daysPassed > 0 ? expense / daysPassed : 0,
                AverageIncome = daysPassed > 0 ? income / daysPassed : 0,
                Period = startDate.Year.ToString(),
                TransactionCount = yearTransactions.Count
            });
        }
        
        public Task<List<CategoryExpense>> GetTopExpenseCategories()
        {
            var startDate = DateTime.Today.AddDays(-30);
            var recentExpenses = _demoTransactions
                .Where(t => t.Date >= startDate && t.Category?.Type == "Expense")
                .ToList();
                
            var totalExpense = recentExpenses.Sum(t => t.Amount);
            
            var topCategories = recentExpenses
                .GroupBy(t => new { t.Category.CategoryId, t.Category.Title, t.Category.Icon })
                .Select(g => new CategoryExpense
                {
                    CategoryName = g.Key.Title,
                    Icon = g.Key.Icon,
                    Amount = g.Sum(t => t.Amount),
                    Percentage = totalExpense > 0 ? (g.Sum(t => t.Amount) * 100m / totalExpense) : 0,
                    TransactionCount = g.Count()
                })
                .OrderByDescending(c => c.Amount)
                .Take(5)
                .ToList();
                
            return Task.FromResult(topCategories);
        }
        
        public Task<decimal> GetSavingsRate()
        {
            var startDate = DateTime.Today.AddMonths(-1);
            var recentTransactions = _demoTransactions
                .Where(t => t.Date >= startDate)
                .ToList();
                
            var income = recentTransactions.Where(t => t.Category?.Type == "Income").Sum(t => t.Amount);
            var expense = recentTransactions.Where(t => t.Category?.Type == "Expense").Sum(t => t.Amount);
            
            if (income == 0) return Task.FromResult(0m);
            
            var savings = income - expense;
            return Task.FromResult(Math.Round((savings * 100m) / income, 1));
        }
        
        public Task<List<ExpenseBreakdown>> GetExpenseBreakdown()
        {
            var startDate = DateTime.Today.AddDays(-30);
            var expenseBreakdown = _demoTransactions
                .Where(t => t.Date >= startDate && t.Category?.Type == "Expense")
                .GroupBy(t => new { t.Category.Title, t.Category.Icon })
                .Select(g => new ExpenseBreakdown
                {
                    Category = g.Key.Title,
                    Icon = g.Key.Icon,
                    Amount = g.Sum(t => t.Amount)
                })
                .ToList();
                
            return Task.FromResult(expenseBreakdown);
        }
        
        public Task<List<RecentTransaction>> GetRecentTransactions()
        {
            var recentTransactions = _demoTransactions
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.TransactionId)
                .Take(5)
                .Select(t => new RecentTransaction
                {
                    Date = t.Date,
                    CategoryName = t.Category.Title,
                    Icon = t.Category.Icon,
                    Amount = t.Amount,
                    Type = t.Category.Type,
                    Note = t.Note ?? ""
                })
                .ToList();
                
            return Task.FromResult(recentTransactions);
        }
    }
}