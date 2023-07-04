using AutoMapper;
using Microsoft.AspNetCore.Http;
using static Website.Shared.Common.CoreEnum;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Bal.Managers
{
    public class GalleryManager : BaseManager<Gallery, GalleryInputModel, GalleryOutputModel, int>, IGalleryManager
    {
        private readonly IBaseRepository<Gallery, int> _baseRepository;
        private readonly IFileManager _fileManager;
        public GalleryManager(
            IFileManager fileManager,
            IBaseRepository<Gallery, int> baseRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _fileManager = fileManager;
        }

        public override async Task<(int statusCode, string message, GalleryOutputModel output)> CreateAsync(GalleryInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _baseRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, GalleryOutputModel output)> UpdateAsync(int id, GalleryInputModel input, int userId)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, GalleryOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public override async Task<BasePaginationOutputModel<GalleryOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _baseRepository.GetListAsync(input);
            return new BasePaginationOutputModel<GalleryOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new GalleryOutputModel(s)).ToList()
            };
        }
    }
}