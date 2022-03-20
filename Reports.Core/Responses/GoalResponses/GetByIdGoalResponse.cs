using Reports.Core.Models;

namespace Reports.Core.Responses.GoalResponses
{
    public class GetByIdGoalResponse
    {
        public Goal Goal { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}