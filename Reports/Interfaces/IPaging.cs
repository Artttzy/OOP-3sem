using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    public interface IPaging<T>
    {
        Task<List<T>> GetByPageAsync(int page);
    }
}