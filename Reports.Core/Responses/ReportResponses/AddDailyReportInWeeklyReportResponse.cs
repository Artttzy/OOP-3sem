using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class AddDailyReportInWeeklyReportResponse
    {
        public WeeklyReport WeeklyReport { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}