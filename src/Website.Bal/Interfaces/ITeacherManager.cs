using Website.Bal.Bases.Interfaces;
using Website.Entity.Models;
using Website.Shared.Entities;

namespace Website.Bal.Interfaces
{
    public interface ITeacherManager : IBaseManager<Teacher, TeacherInputModel, TeacherOutputModel, int>
    {
        Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage, int userId);
        Task<(int statusCode, string message)> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage, int userId);
    }
}
