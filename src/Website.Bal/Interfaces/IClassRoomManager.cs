using Website.Bal.Bases.Interfaces;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Bal.Interfaces
{
    public interface IClassRoomManager : IBaseManager<ClassRoom, ClassRoomInputModel, ClassRoomOutputModel, int>
    {
        Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage, int userId);
        Task<(int statusCode, string message)> SetIsDisplayClassRoomPageAsync(int id, bool isDisplayClassRoomPage, int userId);
    }
}
