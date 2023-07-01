using Website.Entity.Model;
using Website.Shared.Bases.Models;

namespace Website.Bal.Interfaces
{
    public interface IParentManager
    {
        Task CreateAsync(ParentInputModel input, int userId);
        Task UpdateAsync(ParentInputModel input, int userId);
        Task<ParentOutputModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<BasePageOutputModel<ParentOutputModel>> GetListAsync(BasePageInputModel input);
        Task<bool> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage);
    }
}
