using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Repositories
{
    public class FacilityRepository : BaseRepository<Facility, int>, IFacilityRepository
    {
        public FacilityRepository(ApplicationDbContext context) : base(context) { }
    }
}
