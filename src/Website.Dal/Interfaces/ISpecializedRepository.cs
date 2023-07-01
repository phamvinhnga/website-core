using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface ISpecializedRepository
    {
        Task<int> CountNumberTeacherBySpecializedId(int id);
        Task<Specialized> CreateAsync(Specialized input);
        Task UpdateAsync(Specialized input);
        Task<Specialized> GetByIdAsync(int id);
        Task DeleteAsync(Specialized input);
        Task<BasePageOutputModel<Specialized>> GetListAsync(BasePageInputModel input);
    }
}
