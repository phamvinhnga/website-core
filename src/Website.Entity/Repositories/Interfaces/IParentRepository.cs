using Website.Entity.Entities;
using Website.Entity.Model;

namespace Website.Entity.Repositories.Interfaces
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
