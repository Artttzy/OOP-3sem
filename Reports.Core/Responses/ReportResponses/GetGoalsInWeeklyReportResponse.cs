using System.Collections.Generic;
using Reports.Core.Models;

namespace Reports.Core.Responses.ReportResponses
{
    public class GetGoalsInWeeklyReportResponse
    {
        public List<Goal> Goals { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}