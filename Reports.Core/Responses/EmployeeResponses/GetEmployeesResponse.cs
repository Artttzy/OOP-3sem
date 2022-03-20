using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Models;

namespace Reports.Core.Responses
{
    public class GetEmployeesResponse
    {
        public List<Employee> Employees { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}