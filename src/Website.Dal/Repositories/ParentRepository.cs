using Website.Dal.Interfaces;
using Website.Dal;

namespace Website.Entity.Repositories
{
    public class ParentRepository : IParentRepository
    {
        public ParentRepository(
            ApplicationDbContext context
        )
        {
        }
    }
}
