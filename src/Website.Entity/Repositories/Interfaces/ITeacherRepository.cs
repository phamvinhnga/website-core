using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Entity.Entities;
using Website.Entity.Model;

namespace Website.Entity.Repositories.Interfaces
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
