using Microsoft.AspNetCore.SignalR;
using RealtimeDataApp.Data;

namespace RealtimeDataApp.Hubs
{
    public class EmployeeHub : Hub
    {
        public async Task RefreshEmployees(List<Employee> employees)
        {

            await Clients.All.SendAsync("RefreshEmployees", employees);
        }
    }
}
