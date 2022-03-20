using System.Collections.Generic;
using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class GetDailyReportsResponse
    {
        public List<DailyReport> DailyReports { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}