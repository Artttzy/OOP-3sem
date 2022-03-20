namespace Reports.Core.Requests
{
    public class CreateEmployee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? ManagerId { get; set; }
    }
}