namespace Reports.Core.Requests
{
    public class GetEmployees
    {
        public int Page { get; set; }
        public string Surname { get; set; }
        public int? ManagerId { get; set; }
    }
}
