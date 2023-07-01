using System.Linq;
using System.Threading.Tasks;
using Website.Shared.Bases.Models;

namespace Website.Shared.Bases.Interfaces
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        IQueryable<TEntity> Queryable { get; }
        Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true);
        Task<TEntity> UpdateAsync(TEntity input, bool saveChanges = true);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity, bool saveChanges = true);
        Task<BasePaginationOutputModel<TEntity>> GetListAsync(BasePaginationInputModel input);
    }
}
