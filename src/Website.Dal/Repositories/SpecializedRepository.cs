using Microsoft.EntityFrameworkCore;
using Website.Dal.Interfaces;
using Website.Shared.Entities;
using Website.Dal.Bases.Repository;
using Website.Dal;

namespace Website.Entity.Repositories
{
    public class SpecializedRepository : BaseRepository<Specialized, int>, ISpecializedRepository
    {
        private readonly ApplicationDbContext _context;

        public SpecializedRepository(
            ApplicationDbContext context
        ) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountNumberTeacherBySpecializedId(int id)
        {
            return await _context.Teacher.Where(w => w.SpecializedId == id).CountAsync();
        }
    }
}
