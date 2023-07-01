using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Entity.Model;
using Website.Shared.Bases.Models;

namespace Website.Bal.Interfaces
{
    public interface ITeacherManager
    {
        Task CreateAsync(TeacherInputModel input, int userId);
        Task UpdateAsync(TeacherInputModel input, int userId);
        Task<TeacherOutputModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<BasePageOutputModel<TeacherOutputModel>> GetListAsync(BasePageInputModel input);
        Task<bool> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage);
        Task<bool> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage);
    }
}
