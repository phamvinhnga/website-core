using Website.Bal.Bases.Interfaces;
using Website.Entity.Models;
using Website.Shared.Entities;

namespace Website.Bal.Interfaces
{
    public interface IParentManager : IBaseManager<Parent, ParentInputModel, ParentOutputModel, int>
    {
        Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage);
    }
}
