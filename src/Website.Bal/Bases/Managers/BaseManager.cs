using static Website.Shared.Common.CoreEnum;
using Website.Shared.Extensions;
using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Bases.Interfaces;
using Website.Dal.UnitOfWorks;

namespace Website.Dal.Bases.Managers
{
    public class BaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey> : IBaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TInputModel : class
        where TOutputModel : class
        where TPrimaryKey : struct
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseManager(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<(int statusCode, string message, TOutputModel output)> CreateAsync(TInputModel input, int userId)
        {
            var entity = _mapper.Map<TInputModel, TEntity>(input);
            entity.SetCreateDefault(userId);
            await _unitOfWork.GetRepository<TEntity, TPrimaryKey>().CreateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success), _mapper.Map<TEntity, TOutputModel>(entity));
        }

        public virtual async Task<(int statusCode, string message, TOutputModel output)> UpdateAsync(TPrimaryKey id, TInputModel input, int userId)
        {
            var entity = await  _unitOfWork.GetRepository<TEntity, TPrimaryKey>().GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }
            entity = _mapper.Map<TInputModel, TEntity>(input, entity);
            entity.SetModifyDefault(userId);
            await _unitOfWork.GetRepository<TEntity, TPrimaryKey>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success), _mapper.Map<TEntity, TOutputModel>(entity));
        }

        public virtual async Task<(int statusCode, string message, TOutputModel output)> GetByIdAsync(TPrimaryKey id)
        {
            var entity = await  _unitOfWork.GetRepository<TEntity, TPrimaryKey>().GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), _mapper.Map<TEntity, TOutputModel>(entity));
        }

        public virtual async Task<(int statusCode, string message)> DeleteAsync(TPrimaryKey id)
        {
            var entity = await  _unitOfWork.GetRepository<TEntity, TPrimaryKey>().GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            await  _unitOfWork.GetRepository<TEntity, TPrimaryKey>().DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public virtual async Task<BasePaginationOutputModel<TOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await  _unitOfWork.GetRepository<TEntity, TPrimaryKey>().GetListAsync(input);
            return new BasePaginationOutputModel<TOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = _mapper.Map<IList<TEntity>, IList<TOutputModel>>(data.Items)
            };
        }
    }
}
