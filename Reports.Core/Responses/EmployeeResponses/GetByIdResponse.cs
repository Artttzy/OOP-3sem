using Reports.Core.Models;

namespace Reports.Core.Responses
{
    public class GetByIdResponse
    {
        public Employee Employee { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}