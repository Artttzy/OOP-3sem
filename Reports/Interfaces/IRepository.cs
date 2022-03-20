using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.Core.Models;

namespace Reports.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task RemoveAsync(T obj);
        Task<List<T>> GetByPageAsync(int page);
        Task<List<T>> FindAllAsync(Func<T, bool> predicate);
        Task<List<T>> GetFilteredPageAsync(Func<T, bool> predicate, int page);
    }
}
