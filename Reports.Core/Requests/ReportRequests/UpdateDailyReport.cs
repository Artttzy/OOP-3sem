using Reports.Core.Models;

namespace Reports.Core.Requests.ReportRequests
{
    public class UpdateDailyReport
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Content { get; set; }
        public int ChangerId { get; set; }
    }
}