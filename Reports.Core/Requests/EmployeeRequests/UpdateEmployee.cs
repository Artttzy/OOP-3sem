namespace Reports.Core.Requests
{
    public class UpdateEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? ManagerId { get; set; }

        public UpdateEmployee()
        {
            
        }
    }
}
