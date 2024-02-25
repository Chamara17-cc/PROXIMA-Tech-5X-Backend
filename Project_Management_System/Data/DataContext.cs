namespace Project_Management_System.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        //Add Your DbContext here Ex: public DbSet<Budget> Budgets { get; set; }

    }
}
