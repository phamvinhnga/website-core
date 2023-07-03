﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Interfaces;
using Website.Shared.Bases.Models;
using static Website.Shared.Bases.Enums.BaseEnum;

namespace Website.Shared.Bases.Repository
{
    public partial class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey> 
        where TPrimaryKey : struct
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

        public virtual async Task<BasePaginationOutputModel<TEntity>> GetListAsync(BasePaginationInputModel input)
        {
            var query = Queryable;

            if (input.ListCriterias != null && input.ListCriterias.Count > 0)
            {

                foreach (var item in input.ListCriterias)
                {

                    query = item.Option switch
                    {
                        OptionCriteriaRequest.Equals => query.Where(a => EF.Property<string>(a, item.Property) == item.Value),
                        OptionCriteriaRequest.NotEquals => query.Where(a => EF.Property<string>(a, item.Property) != item.Value),
                        OptionCriteriaRequest.Contains => query.Where(a => EF.Property<string>(a, item.Property).Contains(item.Value)),
                        OptionCriteriaRequest.StartsWith => query.Where(a => EF.Property<string>(a, item.Property).StartsWith(item.Value)),
                        _ => throw new System.NotImplementedException(),
                    };
                }
            }

            if (input.Sorting != null)
            {
                var arr = input.Sorting.Split(" ");
                var property = arr[0];
                var typeSorting = arr[1];
                query = typeSorting switch
                {
                    nameof(OptionSort.Asc) => query.OrderBy(a => EF.Property<string>(a, property)),
                    nameof(OptionSort.Desc) => query.OrderByDescending(a => EF.Property<string>(a, property)),
                    _ => throw new System.NotImplementedException(),
                };
            }

            var items = await query.Skip(input.SkipCount).Take(input.MaxCountResult).ToListAsync();

            return new BasePaginationOutputModel<TEntity>() { Items = items, TotalCount = await query.CountAsync() };
        }
    }
}
