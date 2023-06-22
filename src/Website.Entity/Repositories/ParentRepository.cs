using Website.Entity.Entities;
using Website.Entity.Model;
using Website.Entity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Entity.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly ApplicationDbContext _context;

        public ParentRepository(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public Task<Parent> GetByIdAsync(int id)
        {
            return _context.Parent.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<BasePageOutputModel<Parent>> GetListAsync(BasePageInputModel input)
        {
            var query = _context.Parent.AsNoTracking().Where(w => w.Name.StartsWith(input.Search)).OrderBy(o => o.Index);

            var count = await query.CountAsync();

            var items = await query.Skip(input.SkipCount).Take(input.MaxCountResult).ToListAsync();

            return new BasePageOutputModel<Parent>(count, items);
        }

        public async Task<Parent> CreateAsync(Parent input)
        {
            await _context.Parent.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<int> DeleteAsync(Parent input)
        {
            _context.Parent.Remove(input);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Parent input)
        {
            _context.Attach(input);
            _context.Entry(input).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
