namespace Reports.Core.Requests.ReportRequests
{
    public class AddDailyReportInWeeklyReport
    {
        public int Id { get; set; }
        public int DailyId { get; set; }
        public string Change { get; set; }
        public int ChangerId { get; set; }
    }
}