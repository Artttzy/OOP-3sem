using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL;
using Reports.Interfaces;
using Reports.Core.Models;
using Reports.Exceptions;

namespace Reports.Services.Repositories.Employees
{
    public class EmployeeRepository : IRepository<Employee>, IPagedFilter<Employee>
    {
        public ReportContext Context { get; }
        
        public EmployeeRepository(ReportContext context)
        {
            Context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await Context.Employees
                .AsNoTracking()
                .Include(e => e.Manager)
                .Include(e => e.Slaves)
                .ThenInclude(s => s.Goals)
                .Include(d => d.DailyReports)
                .Include(e => e.Slaves)
                .ThenInclude(s => s.DailyReports)
                .Include(e => e.Goals)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(Employee obj)
        {
            try
            {
                await Context.Employees.AddAsync(obj);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new CreateEmployeeException(e.Message);
            }
        }

        public async Task UpdateAsync(Employee obj)
        {
            Context.Employees.Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Employee obj)
        {
            Context.Employees.Remove(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetByPageAsync(int page)
        {
            return await Context.Employees
                .Include(e => e.Manager)
                .Include(e => e.Goals)
                .Include(e => e.Slaves).Skip((page - 1) * 10).Take(10).ToListAsync();
        }

        public async Task<List<Employee>> FindAllAsync(Func<Employee, bool> predicate)
        {
            return await Context.Employees.Where(employee => predicate(employee)).ToListAsync();
        }

        public Task<List<Employee>> GetFilteredPageAsync(Func<Employee, bool> predicate, int page)
        {
            return Task.FromResult(Context.Employees
                .Include(e => e.Manager)
                .Include(e => e.Goals)
                .Include(e => e.Slaves).AsEnumerable().Where(employee => predicate(employee)).Skip((page - 1) * 10).Take(10).ToList());
        }
    }
}