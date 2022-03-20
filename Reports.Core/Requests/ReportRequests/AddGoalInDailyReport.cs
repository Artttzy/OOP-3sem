namespace Reports.Core.Requests.ReportRequests
{
    public class AddGoalInDailyReport
    {
        public int Id { get; set; }
        public int GoalId { get; set; }
        public string Change { get; set; }
        public int ChangerId { get; set; }
    }
}