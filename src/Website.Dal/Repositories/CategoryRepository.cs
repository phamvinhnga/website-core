using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Repositories
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
