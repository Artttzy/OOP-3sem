using Reports.Core.Models;

namespace Reports.Core.Requests.GoalRequests
{
    public class GetSlavesGoals
    {
        public int Page { get; set; }
        public int OwnerId { get; set; }
    }
}