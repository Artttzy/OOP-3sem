using Reports.Core.Models;

namespace Reports.Core.Responses
{
    public class UpdateEmployeeResponse
    {
        public Employee Employee { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}