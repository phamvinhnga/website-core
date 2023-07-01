using AutoMapper;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
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

        public async Task CreateAsync(TeacherInputModel input, int userId)
        {
            var entity = _mapper.Map<Teacher>(input);
            entity.SetCreateDefault(userId);
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                var file = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
                entity.Thumbnail = file.ConvertToJson();
            }
            else
            {
                entity.Thumbnail = null;
            }
            await _teacherRepository.CreateAsync(entity);
        }

        public async Task UpdateAsync(TeacherInputModel input, int userId)
        {
            var entity = await _teacherRepository.GetByIdAsync(input.Id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find TeacherId {input.Id}");
            }
            _mapper.Map(input, entity);
            entity.SetModifyDefault(userId);
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                var file = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
                entity.Thumbnail = file.ConvertToJson();
            }
            else if (input.Thumbnail == null)
            {
                entity.Thumbnail = null;
            }
            await _teacherRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _teacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find TeacherId {id}");
            }
            await _teacherRepository.DeleteAsync(entity);
        }

        public async Task<bool> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var query = await _teacherRepository.GetByIdAsync(id);
            if(query == null)
            {
                throw new BadRequestException($"Cannot find TeacherId {id}");
            }
            query.IsDisplayIndexPage = isDisplayIndexPage;
            return await _teacherRepository.UpdateAsync(query) > 0;
        }
        
        public async Task<bool> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage)
        {
            var query = await _teacherRepository.GetByIdAsync(id);
            if(query == null)
            {
                throw new BadRequestException($"Cannot find TeacherId {id}");
            }
            query.IsDisplayTeacherPage = isDisplayTeacherPage;
            return await _teacherRepository.UpdateAsync(query) > 0;
        }
        
        public async Task<TeacherOutputModel> GetByIdAsync(int id)
        {
            var query = await _teacherRepository.GetByIdAsync(id);
            if (query == null)
            {
                throw new BadRequestException($"Cannot find TeacherId {id}");
            }
            return _mapper.Map<TeacherOutputModel>(query);
        }

        public async Task<BasePageOutputModel<TeacherOutputModel>> GetListAsync(BasePageInputModel input)
        {
            var query = await _teacherRepository.GetListAsync(input);
            return new BasePageOutputModel<TeacherOutputModel>(query.TotalItem, _mapper.Map<List<TeacherOutputModel>>(query.Items));
        }
    }
}
