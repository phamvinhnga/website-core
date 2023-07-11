using AutoMapper;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.UnitOfWorks;
using Website.Entity.Models;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Bal.Managers
{
    public class CategoryManager : BaseManager<Category, CategoryInputModel, CategoryOutputModel, int>, ICategoryManager
    {
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
