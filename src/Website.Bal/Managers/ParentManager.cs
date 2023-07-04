using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class ParentManager : BaseManager<Parent, ParentInputModel, ParentOutputModel, int>, IParentManager
    {
        private readonly IBaseRepository<Parent, int> _baseRepository;
        private readonly IFileManager _fileManager;
        public ParentManager(
            IFileManager fileManager,
            IBaseRepository<Parent, int> baseRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _fileManager = fileManager;
        }

        public override async Task<(int statusCode, string message, ParentOutputModel output)> CreateAsync(ParentInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _baseRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, ParentOutputModel output)> UpdateAsync(int id, ParentInputModel input, int userId)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public override async Task<(int statusCode, string message, ParentOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public override async Task<BasePaginationOutputModel<ParentOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _baseRepository.GetListAsync(input);
            return new BasePaginationOutputModel<ParentOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new ParentOutputModel(s)).ToList()
            };
        }
    }
}
