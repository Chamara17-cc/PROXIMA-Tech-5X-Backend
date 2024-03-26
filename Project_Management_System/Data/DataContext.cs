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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<DeveloperRate> DeveloperRates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DeveloperFinancialRecipt> developerFinancialRecipts { get; set; }
        public DbSet<DeveloperProject> DeveloperProjects { get; set; }
        public DbSet<FileResource> FileResources { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ViewInvoice> ViewInvoices { get; set; }
        public DbSet<ViewReport> ViewReports { get; set; }
        public DbSet<ViewResource> ViewResources { get; set; }
    }
}