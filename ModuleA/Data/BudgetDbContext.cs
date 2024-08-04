using Microsoft.EntityFrameworkCore;

namespace ModuleA.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            : base(options)
        {
        }

        public DbSet<BudgetCategoryEntry> BudgetCategoryEntries { get; set; }
        public DbSet<MonthlyIncome> MonthlyIncomes { get; set; }
    }

    public class BudgetCategoryEntry
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
    }

    public class MonthlyIncome
    {
        public int Id { get; set; }
        public decimal Income { get; set; }
    }
}
