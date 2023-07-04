using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Repositories
{
    public class ClassRoomRepository : BaseRepository<ClassRoom, int>, IClassRoomRepository
    {
        public ClassRoomRepository(ApplicationDbContext context) : base(context) { }
    }
}
