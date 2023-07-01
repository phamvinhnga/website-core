using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface IParentRepository
    {
        Task<Parent> CreateAsync(Parent input);
        Task<int> UpdateAsync(Parent input);
        Task<Parent> GetByIdAsync(int id);
        Task<int> DeleteAsync(Parent input);
        Task<BasePageOutputModel<Parent>> GetListAsync(BasePageInputModel input);
    }
}
