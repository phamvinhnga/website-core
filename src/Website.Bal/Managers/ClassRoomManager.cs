using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.UnitOfWorks;
using Website.Shared.Entities;
using Website.Shared.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Bal.Managers
{
    public class ClassRoomManager : BaseManager<ClassRoom, ClassRoomInputModel, ClassRoomOutputModel, int>, IClassRoomManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public ClassRoomManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public new async Task<(int statusCode, string message, ClassRoomOutputModel output)> CreateAsync(ClassRoomInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.ClassRoom);
            }
            return await base.CreateAsync(input, userId);
        }

        public new async Task<(int statusCode, string message, ClassRoomOutputModel output)> UpdateAsync(int id, ClassRoomInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.ClassRoom);
            }
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage, int userId)
        {
            var entity = await _unitOfWork.ClassRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            entity.SetModifyDefault(userId);
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayClassRoomPageAsync(int id, bool isDisplayClassRoomPage, int userId)
        {
            var entity = await _unitOfWork.ClassRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayClassRoomPage = isDisplayClassRoomPage;
            entity.SetModifyDefault(userId);
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}