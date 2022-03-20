namespace Reports.Core.Requests.ReportRequests
{
    public class GetSlavesDailyReports
    {
        public int OwnerId { get; set; }
        public bool Readiness { get; set; }
    }
}