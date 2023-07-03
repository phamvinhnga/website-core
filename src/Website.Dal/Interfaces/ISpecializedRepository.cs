using Website.Dal.Bases.Interfaces;
using Website.Shared.Bases.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface ISpecializedRepository : IBaseRepository<Specialized, int>
    {
        Task<int> CountNumberTeacherBySpecializedId(int id);
    }
}
