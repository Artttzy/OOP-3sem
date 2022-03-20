using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class CreateDailyReportResponse
    {
        public DailyReport DailyReport { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}