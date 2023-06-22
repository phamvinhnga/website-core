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
    public class SpecializedRepository : ISpecializedRepository
    {
        private readonly ApplicationDbContext _context;

        public SpecializedRepository(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<int> CountNumberTeacherBySpecializedId(int id)
        {
            return await _context.Teacher.AsNoTracking().Where(w => w.SpecializedId == id).CountAsync();
        }

        public async Task<Specialized> GetByIdAsync(int id)
        {
            return await _context.Specialized.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<BasePageOutputModel<Specialized>> GetListAsync(BasePageInputModel input)
        {
            var query = _context.Specialized.AsNoTracking().Where(w => w.Name.StartsWith(input.Search));

            var count = await query.CountAsync();

            var items = await query.Skip(input.SkipCount).Take(input.MaxCountResult).ToListAsync();

            return new BasePageOutputModel<Specialized>(count, items);
        }

        public async Task<Specialized> CreateAsync(Specialized input)
        {
            await _context.Specialized.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public Task DeleteAsync(Specialized input)
        {
            return Task.Run(async () => {
                _context.Specialized.Remove(input);
                await _context.SaveChangesAsync();
            });
        }

        public Task UpdateAsync(Specialized input)
        {
            return Task.Run(async () => {
                _context.Attach(input);
                _context.Entry(input).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            });
        }
    }
}
