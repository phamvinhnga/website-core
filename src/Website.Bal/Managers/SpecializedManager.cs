using AutoMapper;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.UnitOfWorks;
using Website.Entity.Models;
using Website.Shared.Entities;

namespace Website.Bal.Managers
{
    public class SpecializedManager : BaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>, ISpecializedManager
    {
        public SpecializedManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
