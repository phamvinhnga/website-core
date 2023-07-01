using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Shared.Bases.Repository
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        IQueryable<TEntity> Queryable { get; }
        Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true);
        Task<TEntity> UpdateAsync(TEntity input, bool saveChanges = true);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity, bool saveChanges = true);
    }
}
