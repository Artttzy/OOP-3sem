using Reports.Core.Models;

namespace Reports.Core.Requests.ReportRequests
{
    public class CreateWeeklyReportResponse
    {
        public WeeklyReport WeeklyReport { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}