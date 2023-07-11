using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Shared.Models;
using Website.Shared.Entities;
using Website.Dal.Bases.Managers;
using AutoMapper;
using Website.Dal.UnitOfWorks;
using static Website.Shared.Common.CoreEnum;

namespace Website.Bal.Managers
{
    public class GalleryManager : BaseManager<Gallery, GalleryInputModel, GalleryOutputModel, int>, IGalleryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public GalleryManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public new async Task<(int statusCode, string message, GalleryOutputModel output)> CreateAsync(GalleryInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            return await base.CreateAsync(input, userId);
        }

        public new async Task<(int statusCode, string message, GalleryOutputModel output)> UpdateAsync(int id, GalleryInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Gallery);
            }
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message)> SetIsDisplayGalleryPageAsync(int id, bool isDisplayGalleryPage, int userId)
        {
            var entity = await _unitOfWork.GalleryRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayGalleryPage = isDisplayGalleryPage;
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}