namespace Reports.Core.Requests
{
    public class DeleteEmployee
    {
        public int Id { get;  }

        public DeleteEmployee(int id)
        {
            Id = id;
        }
    }
}