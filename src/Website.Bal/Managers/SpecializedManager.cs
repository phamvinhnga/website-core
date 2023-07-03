using AutoMapper;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Shared.Bases.Interfaces;
using Website.Shared.Bases.Managers;
using Website.Shared.Entities;

namespace Website.Biz.Managers
{
    public class SpecializedManager : BaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>, ISpecializedManager
    {
        public SpecializedManager(
             IBaseRepository<Specialized, int> baseRepository,
             IMapper mapper
        ) : base(baseRepository, mapper)
        {
        }
    }
}
