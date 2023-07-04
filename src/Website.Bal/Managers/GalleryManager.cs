using Microsoft.AspNetCore.Http;
using static Website.Shared.Common.CoreEnum;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Models;
using Website.Dal.Interfaces;
using Website.Shared.Extensions;

namespace Website.Bal.Managers
{
    public class GalleryManager : IGalleryManager
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IFileManager _fileManager;
        public GalleryManager(
            IFileManager fileManager,
            IGalleryRepository repository
        ) 
        {
            _galleryRepository = repository;
            _fileManager = fileManager;
        }

        public async Task<(int statusCode, string message, GalleryOutputModel output)> CreateAsync(GalleryInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _galleryRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public async Task<(int statusCode, string message, GalleryOutputModel output)> UpdateAsync(int id, GalleryInputModel input, int userId)
        {
            var entity = await _galleryRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _galleryRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public async Task<(int statusCode, string message, GalleryOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _galleryRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new GalleryOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<GalleryOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _galleryRepository.GetListAsync(input);
            return new BasePaginationOutputModel<GalleryOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new GalleryOutputModel(s)).ToList()
            };
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _galleryRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}