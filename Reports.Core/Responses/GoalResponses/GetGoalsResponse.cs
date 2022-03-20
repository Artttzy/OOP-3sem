using System.Collections.Generic;
using Reports.Core.Models;

namespace Reports.Core.Responses.GoalResponses
{
    public class GetGoalsResponse
    {
        public List<Goal> Goals { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}