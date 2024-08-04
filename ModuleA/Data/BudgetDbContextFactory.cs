using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ModuleA.Data
{
    public class BudgetDbContextFactory
    {
        public static BudgetDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BudgetDbContext>();
            optionsBuilder.UseSqlite("Data Source=budget.db");

            return new BudgetDbContext(optionsBuilder.Options);
        }
    }
}
