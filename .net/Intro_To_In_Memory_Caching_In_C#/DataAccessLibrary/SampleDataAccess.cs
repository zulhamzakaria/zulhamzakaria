using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();
            output.Add(new() { FirstName = "Jim", LastName = "Carrey" });
            output.Add(new() { FirstName = "Tony", LastName = "Hawk" });
            output.Add(new() { FirstName = "Brad", LastName = "Pitt" });
            Thread.Sleep(3000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();
            output.Add(new() { FirstName = "Jim", LastName = "Carrey" });
            output.Add(new() { FirstName = "Tony", LastName = "Hawk" });
            output.Add(new() { FirstName = "Brad", LastName = "Pitt" });
            await Task.Delay(3000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeeCache()
        {
            List<EmployeeModel> output;

            output = memoryCache.Get<List<EmployeeModel>>("employees");

            if(output == null)
            {
                output = new();
                output.Add(new() { FirstName = "Jim", LastName = "Carrey" });
                output.Add(new() { FirstName = "Tony", LastName = "Hawk" });
                output.Add(new() { FirstName = "Brad", LastName = "Pitt" });
                await Task.Delay(3000);

                memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }
    }
}
