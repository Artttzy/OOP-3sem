using Reports.Core.Models;

namespace Reports.Core.Requests.GoalRequests
{
    public class UpdateGoal
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; set; }
        public int ChangerId { get; set; }
    }
}
