using Reports.Core.Models;

namespace Reports.Core.Requests.ReportRequests
{
    public class UpdateWeeklyReport
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Content { get; set; }
        public string Change { get; set; }
        public int ChangerId { get; set; }
    }
}