using System;

namespace Reports.Exceptions.ReportExceptions
{
    public class GetByIdReportException : Exception
    {
        public GetByIdReportException(string message) : base(message)
        {
            
        }
    }
}