using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.Core.Models;
using Reports.DAL;
using Reports.Exceptions.GoalExceptions;
using Reports.Interfaces;

namespace Reports.Services.Repositories.Goals
{
    public class GoalRepository : IRepository<Goal>, IPagedFilter<Goal>
    {
        public ReportContext Context { get; }
        
        public GoalRepository(ReportContext context)
        {
            Context = context;
        }
        
        public async Task<List<Goal>> GetAllAsync()
        {
            return await Context.Goals.ToListAsync();
        }

        public async Task<Goal> GetByIdAsync(int id)
        {
            return await Context.Goals.AsNoTracking().Include(g => g.Owner).Include(g => g.Changes).FirstOrDefaultAsync(e => e.Id == id);;
        }

        public async Task CreateAsync(Goal obj)
        {
            try
            {
                await Context.Goals.AddAsync(obj);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new CreateGoalException(e.Message);
            }
        }

        public async Task UpdateAsync(Goal obj)
        {
            Context.Goals.Update(obj);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Goal obj)
        {
            Context.Goals.Remove(obj);

            await Context.SaveChangesAsync();
        }

        public async Task<List<Goal>> GetByPageAsync(int page)
        {
            return await Context.Goals.Skip((page - 1) * 10).Take(10).ToListAsync();
        }

        public async Task<List<Goal>> FindAllAsync(Func<Goal, bool> predicate)
        {
            return await Context.Goals.Where((employee, i) => predicate(employee)).ToListAsync();
        }

        public Task<List<Goal>> GetFilteredPageAsync(Func<Goal, bool> predicate, int page)
        {
            return Task.FromResult(Context.Goals.Include(g => g.Changes).AsEnumerable().Where(predicate).Skip((page - 1) * 10).Take(10).ToList());
        }
    }
}