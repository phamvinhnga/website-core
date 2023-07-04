using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Repositories
{
    public class GalleryRepository : BaseRepository<Gallery, int>, IGalleryRepository
    {
        public GalleryRepository(ApplicationDbContext context) : base(context) { }
    }
}
