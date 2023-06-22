using Website.Entity.Entities;
using Website.Entity.Model;

namespace Website.Entity.Repositories.Interfaces
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
