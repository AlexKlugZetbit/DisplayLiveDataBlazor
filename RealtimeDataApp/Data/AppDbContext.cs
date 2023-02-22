using Microsoft.EntityFrameworkCore;

namespace RealtimeDataApp.Data
{
    public class AppDbContext : DbContext
    {
        string _connectionString = "Server=DESKTOP-UL9R65A\\SQLEXPRESS;Database=CompanyDatabase2;Trusted_Connection=True;";

        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
