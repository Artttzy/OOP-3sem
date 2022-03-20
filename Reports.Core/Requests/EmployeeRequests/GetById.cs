using System.Net.NetworkInformation;

namespace Reports.Core.Requests
{
    public class GetById
    {
        public int Id { get; }

        public GetById(int id)
        {
            Id = id;
        }
    }
}