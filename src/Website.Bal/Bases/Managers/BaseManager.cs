using static Website.Shared.Common.CoreEnum;
using Website.Shared.Extensions;
using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Models;
using AutoMapper;
using Website.Dal.Bases.Interfaces;
using Microsoft.AspNetCore.Http;
using Website.Bal.Bases.Interfaces;

namespace Website.Dal.Bases.Managers
{
    public class BaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey> : IBaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TInputModel : class
        where TOutputModel : class
        where TPrimaryKey : struct
    {
        private readonly IBaseRepository<TEntity, TPrimaryKey> _baseRepository;
        private readonly IMapper _mapper;

        public BaseManager(
            IBaseRepository<TEntity, TPrimaryKey> baseRepository,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public virtual async Task<(int statusCode, string message, TOutputModel output)> CreateAsync(TInputModel input, int userId)
        {
            var entity = input.JsonMapTo<TEntity>();
            entity.SetCreateDefault(userId);
            await _baseRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), entity.JsonMapTo<TOutputModel>());
        }

        public virtual async Task<(int statusCode, string message, TOutputModel output)> UpdateAsync(TPrimaryKey id, TInputModel input, int userId)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            entity = _mapper.Map<TInputModel, TEntity>(input, entity);
            entity.SetModifyDefault(userId);
            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), entity.JsonMapTo<TOutputModel>());
        }

        public virtual async Task<(int statusCode, string message, TOutputModel ouput)> GetByIdAsync(TPrimaryKey id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), entity.JsonMapTo<TOutputModel>());
        }

        public virtual async Task<(int statusCode, string message)> DeleteAsync(TPrimaryKey id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            await _baseRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public virtual async Task<BasePaginationOutputModel<TOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _baseRepository.GetListAsync(input);
            return new BasePaginationOutputModel<TOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.JsonMapTo<IList<TOutputModel>>()
            };
        }
    }
}
