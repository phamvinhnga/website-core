using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Models;

namespace Website.Dal.Bases.Interfaces
{
    public interface IBaseRepository<TEntity, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey> 
        where TPrimaryKey : struct
    {
        IQueryable<TEntity> Queryable { get; }
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity input);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity);
        Task<BasePaginationOutputModel<TEntity>> GetListAsync(BasePaginationInputModel input);
    }
}
