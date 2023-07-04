using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Bal.Managers
{
    public class ClassRoomManager : IClassRoomManager
    {
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IFileManager _fileManager;

        public ClassRoomManager(
            IFileManager fileManager,
            IClassRoomRepository classRoomRepository
        ) 
        {
            _classRoomRepository = classRoomRepository;
            _fileManager = fileManager;
        }

        public async Task<(int statusCode, string message, ClassRoomOutputModel output)> CreateAsync(ClassRoomInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.ClassRoom);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _classRoomRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public async Task<(int statusCode, string message, ClassRoomOutputModel output)> UpdateAsync(int id, ClassRoomInputModel input, int userId)
        {
            var entity = await _classRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.ClassRoom);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _classRoomRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public async Task<(int statusCode, string message, ClassRoomOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _classRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<ClassRoomOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _classRoomRepository.GetListAsync(input);
            return new BasePaginationOutputModel<ClassRoomOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new ClassRoomOutputModel(s)).ToList()
            };
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage, int userId)
        {
            var entity = await _classRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            entity.SetModifyDefault(userId);
            await _classRoomRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayClassRoomPageAsync(int id, bool isDisplayClassRoomPage, int userId)
        {
            var entity = await _classRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayClassRoomPage = isDisplayClassRoomPage;
            entity.SetModifyDefault(userId);
            await _classRoomRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _classRoomRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            await _classRoomRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}