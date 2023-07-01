using Microsoft.EntityFrameworkCore;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Dal;
using Website.Shared.Entities;

namespace Website.Entity.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public Task<Teacher> GetByIdAsync(int id)
        {
            return _context.Teacher.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<BasePageOutputModel<Teacher>> GetListAsync(BasePageInputModel input)
        {
            var query = _context.Teacher.AsNoTracking().Where(w => w.Name.StartsWith(input.Search)).OrderBy(o => o.Index);

            var count = await query.CountAsync();

            var items = await query.Skip(input.SkipCount).Take(input.MaxCountResult).ToListAsync();

            return new BasePageOutputModel<Teacher>(count, items);
        }

        public async Task<Teacher> CreateAsync(Teacher input)
        {
            await _context.Teacher.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<int> DeleteAsync(Teacher input)
        {
            _context.Teacher.Remove(input);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Teacher input)
        {
            _context.Attach(input);
            _context.Entry(input).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
