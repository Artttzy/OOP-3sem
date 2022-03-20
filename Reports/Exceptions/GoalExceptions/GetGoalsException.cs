using System;

namespace Reports.Exceptions.GoalExceptions
{
    public class GetGoalsException : Exception
    {
        public GetGoalsException(string message) : base(message)
        {
            
        }
    }
}