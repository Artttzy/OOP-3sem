namespace Reports.Core.Requests.ReportRequests
{
    public class CreateWeeklyReport
    {
        public int OwnerId { get; set; }
        public string Content { get; set; }
    }
}