using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Shared.Bases.Models;
using Website.Shared.Exceptions;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class TeacherManager : ITeacherManager
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public TeacherManager(
            ITeacherRepository teacherRepository,
            IFileManager fileManager,
            IMapper mapper
        )
        {
            _teacherRepository = teacherRepository;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task<(int statusCode, string message, TeacherOutputModel output)> CreateAsync(TeacherInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _teacherRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<(int statusCode, string message, TeacherOutputModel output)> UpdateAsync(int id, TeacherInputModel input, int userId)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found", null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);
   
            await _teacherRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found");
            }
            await _teacherRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _teacherRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message)> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found");
            }
            entity.IsDisplayTeacherPage = isDisplayTeacherPage;
            await _teacherRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message, TeacherOutputModel ouput)> GetByIdAsync(int id)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<TeacherOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _teacherRepository.GetListAsync(input);
            return new BasePaginationOutputModel<TeacherOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new TeacherOutputModel(s)).ToList()
            };
        }
    }
}
