using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeDataApp.Data;
using RealtimeDataApp.Hubs;
using System.Threading.Channels;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace RealtimeDataApp.Services
{
    public class EmployeeService
    {
        private readonly IHubContext<EmployeeHub> _context;
        AppDbContext dbContext = new AppDbContext();
        private readonly SqlTableDependency<Employee> _dependency;
        private readonly string _connectionString;

        public EmployeeService(IHubContext<EmployeeHub> context)
        {
            _context = context;
            _connectionString = "Server=DESKTOP-UL9R65A\\SQLEXPRESS;Database=CompanyDatabase2;Trusted_Connection=True;";
            _dependency = new SqlTableDependency<Employee>(_connectionString, "Employee");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            var employees = await GetAllEmployees();
            await _context.Clients.All.SendAsync("RefreshEmployees", employees);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employee.AsNoTracking().ToListAsync();
        }
    }
}
