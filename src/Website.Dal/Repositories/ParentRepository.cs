using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Repositories
{
    public class ParentRepository : BaseRepository<Parent, int>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext context) : base(context) { }
    }
}
