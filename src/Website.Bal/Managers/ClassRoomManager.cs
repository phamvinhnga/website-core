using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Entity.Model;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Bal.Managers
{
    public class ClassRoomManager : BaseManager<ClassRoom, ClassRoomInputModel, ClassRoomOutputModel, int>, IClassRoomManager
    {
        private readonly IBaseRepository<ClassRoom, int> _baseRepository;
        private readonly IFileManager _fileManager;

        public ClassRoomManager(
            IFileManager fileManager,
            IBaseRepository<ClassRoom, int> baseRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _fileManager = fileManager;
        }

        public override async Task<(int statusCode, string message, ClassRoomOutputModel output)> CreateAsync(ClassRoomInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.ClassRoom);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _baseRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, ClassRoomOutputModel output)> UpdateAsync(int id, ClassRoomInputModel input, int userId)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
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

            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, ClassRoomOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new ClassRoomOutputModel(entity));
        }

        public override async Task<BasePaginationOutputModel<ClassRoomOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _baseRepository.GetListAsync(input);
            return new BasePaginationOutputModel<ClassRoomOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new ClassRoomOutputModel(s)).ToList()
            };
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

        public async Task<(int statusCode, string message)> SetIsDisplayClassRoomPageAsync(int id, bool isDisplayClassRoomPage)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayClassRoomPage = isDisplayClassRoomPage;
            await _baseRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}