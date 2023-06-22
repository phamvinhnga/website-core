using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Entity.Model;

namespace Website.Biz.Managers.Interfaces
{
    public interface ISpecializedManager
    {
        Task CreateAsync(SpecializedInputModel input, int userId);
        Task UpdateAsync(SpecializedInputModel input, int userId);
        Task<SpecializedOutputModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<BasePageOutputModel<SpecializedOutputModel>> GetListAsync(BasePageInputModel input);
    }
}
