using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    public interface IPagedFilter<T> : IPaging<T>, IFilter<T>
    {
        Task<List<T>> GetFilteredPageAsync(Func<T, bool> predicate, int page);
    }
}