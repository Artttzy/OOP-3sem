using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class UpdateWeeklyReportResponse
    {
        public WeeklyReport WeeklyReport { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}