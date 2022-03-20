using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.Core.Models;
using Reports.DAL;
using Reports.Exceptions.ReportExceptions;
using Reports.Interfaces;

namespace Reports.Services.Repositories.Reports
{
    public class DailyReportRepository : IRepository<DailyReport>, IPagedFilter<DailyReport>
    {
        public ReportContext Context { get; }
        
        public DailyReportRepository(ReportContext context)
        {
            Context = context;
        }

        public async Task<List<DailyReport>> GetAllAsync()
        {
            return await Context.DailyReports.ToListAsync();
        }

        public async Task<DailyReport> GetByIdAsync(int id)
        {
            return await Context.DailyReports
                .Include(c => c.Changes)
                .Include(d => d.Goals)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CreateAsync(DailyReport obj)
        {
            try
            {
                await Context.DailyReports.AddAsync(obj);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new CreateReportException(e.Message);
            }
        }

        public async Task UpdateAsync(DailyReport obj)
        {
            Context.DailyReports.Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(DailyReport obj)
        {
            Context.DailyReports.Remove(obj);

            await Context.SaveChangesAsync();
        }

        public async Task<List<DailyReport>> GetByPageAsync(int page)
        {
            return await Context.DailyReports.Skip((page - 1) * 10).Take(10).ToListAsync();
        }

        public async Task<List<DailyReport>> FindAllAsync(Func<DailyReport, bool> predicate)
        {
            return await Context.DailyReports.Where((employee, i) => predicate(employee)).ToListAsync();
        }

        public Task<List<DailyReport>> GetFilteredPageAsync(Func<DailyReport, bool> predicate, int page)
        {
            return Task.FromResult(Context.DailyReports.AsEnumerable().Where(predicate).Skip((page - 1) * 10).Take(10).ToList());
        }
    }
}