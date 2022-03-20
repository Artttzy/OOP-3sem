using System;
using Reports.Core.Models;

namespace Reports.Core.Requests.GoalRequests
{
    public class GetGoals
    {
        public int Page { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public int? OwnerId { get; set; }
        public int? ChangerId { get; set; }
    }
}
