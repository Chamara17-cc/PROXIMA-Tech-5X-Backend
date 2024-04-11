using Project_Management_System.Models;

namespace Project_Management_System.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        //Add Your DbContext here Ex: public DbSet<Budget> Budgets { get; set; }
        public DbSet<DeveloperRate> DeveloperRates { get; set; }

    }
}