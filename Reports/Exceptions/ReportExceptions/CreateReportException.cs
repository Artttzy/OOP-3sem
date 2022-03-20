using System;
namespace Reports.Exceptions.ReportExceptions
{
    public class CreateReportException : Exception
    {

        public CreateReportException(string message) : base(message)
        {
            
        }
    }
}