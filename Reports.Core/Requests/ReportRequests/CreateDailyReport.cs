namespace Reports.Core.Requests.ReportRequests
{
    public class CreateDailyReport
    {
        public int OwnerId { get; set; }
        public string Content { get; set; }
    }
}