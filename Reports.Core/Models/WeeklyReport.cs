using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Models
{
    public class WeeklyReport : Report
    {
        public List<DailyReport> DailyReports = new List<DailyReport>();
    }
}