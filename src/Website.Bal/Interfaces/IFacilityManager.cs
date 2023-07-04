using Website.Bal.Bases.Interfaces;
using Website.Entity.Models;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Bal.Interfaces
{
    public interface IFacilityManager : IBaseManager<Facility, FacilityInputModel, FacilityOutputModel, int>
    {
        Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage);
    }
}
