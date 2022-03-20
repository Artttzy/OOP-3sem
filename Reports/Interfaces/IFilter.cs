using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reports.Interfaces
{
    public interface IFilter<T>
    {
        Task<List<T>> FindAllAsync(Func<T, bool> predicate);
    }
}