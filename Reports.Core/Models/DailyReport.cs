using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Models
{
    public class DailyReport : Report
    {
        public List<Goal> Goals { get; set; } = new List<Goal>();
        
        public int? WeeklyReportId { get; set; }
        public WeeklyReport WeeklyReport { get; set; }
    }
}
