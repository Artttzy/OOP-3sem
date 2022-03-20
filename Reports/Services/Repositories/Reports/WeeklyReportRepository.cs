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
    public class WeeklyReportRepository : IRepository<WeeklyReport>, IPagedFilter<WeeklyReport>
    {
        public ReportContext Context { get; }
        
        public WeeklyReportRepository(ReportContext context)
        {
            Context = context;
        }

        public async Task<List<WeeklyReport>> GetAllAsync()
        {
            return await Context.WeeklyReports.ToListAsync();
        }

        public async Task<WeeklyReport> GetByIdAsync(int id)
        {
            return await Context.WeeklyReports
                .Include(w => w.Owner)
                .Include(d => d.DailyReports)
                .Include(w => w.DailyReports)
                .ThenInclude(d => d.Owner)
                .Include(w => w.DailyReports)
                .ThenInclude(d => d.Goals)
                .Include(w => w.Changes)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CreateAsync(WeeklyReport obj)
        {
            try
            {
                await Context.WeeklyReports.AddAsync(obj);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new CreateReportException(e.Message);
            }
        }

        public async Task UpdateAsync(WeeklyReport obj)
        {
            Context.WeeklyReports.Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(WeeklyReport obj)
        {
            Context.WeeklyReports.Remove(obj);

            await Context.SaveChangesAsync();
        }

        public async Task<List<WeeklyReport>> GetByPageAsync(int page)
        {
            return await Context.WeeklyReports.Skip((page - 1) * 10).Take(10).ToListAsync();
        }

        public async Task<List<WeeklyReport>> FindAllAsync(Func<WeeklyReport, bool> predicate)
        {
            return await Context.WeeklyReports.Where((employee, i) => predicate(employee)).ToListAsync();
        }

        public Task<List<WeeklyReport>> GetFilteredPageAsync(Func<WeeklyReport, bool> predicate, int page)
        {
            return Task.FromResult(Context.WeeklyReports.AsEnumerable().Where(predicate).Skip((page - 1) * 10).Take(10).ToList());
        }
    }
}