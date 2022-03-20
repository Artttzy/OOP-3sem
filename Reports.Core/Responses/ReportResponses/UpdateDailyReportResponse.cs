using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class UpdateDailyReportResponse
    {
        public DailyReport DailyReport { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}