using Website.Dal.Interfaces;
using Website.Shared.Entities;
using Website.Dal.Bases.Repository;
using Website.Dal;

namespace Website.Entity.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher, int>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context) { }
    }
}
