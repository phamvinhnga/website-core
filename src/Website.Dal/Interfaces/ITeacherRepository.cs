using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher> CreateAsync(Teacher input);
        Task<int> UpdateAsync(Teacher input);
        Task<Teacher> GetByIdAsync(int id);
        Task<int> DeleteAsync(Teacher input);
        Task<BasePageOutputModel<Teacher>> GetListAsync(BasePageInputModel input);
    }
}
