using System;

namespace Reports.Exceptions.GoalExceptions
{
    public class GetByIdGoalException : Exception
    {
        public GetByIdGoalException(string message) : base(message)
        {
            
        }
    }
}