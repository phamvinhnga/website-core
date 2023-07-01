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
        Task<(int statusCode, string message, TeacherOutputModel output)> CreateAsync(TeacherInputModel input, int userId);
        Task<(int statusCode, string message, TeacherOutputModel output)> UpdateAsync(int id, TeacherInputModel input, int userId);
        Task<(int statusCode, string message)> DeleteAsync(int id);
        Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage);
        Task<(int statusCode, string message)> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage);
        Task<(int statusCode, string message, TeacherOutputModel ouput)> GetByIdAsync(int id);
        Task<BasePaginationOutputModel<TeacherOutputModel>> GetListAsync(BasePaginationInputModel input);
    }
}
