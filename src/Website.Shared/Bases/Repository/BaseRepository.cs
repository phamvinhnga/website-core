using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Shared.Bases.Repository
{
    public abstract partial class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class where TPrimaryKey : struct
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Queryable => _dbSet;

        public virtual async Task<TEntity> CreateAsync(TEntity entity, bool saveChanges = true)
        {
            await _dbSet.AddAsync(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity input, bool saveChanges = true)
        {
            _dbContext.Attach(input);
            _dbContext.Entry(input).State = EntityState.Modified;
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
            return input;
        }

        public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            _dbSet.Remove(entity);

            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
