using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.UnitOfWorks;
using Website.Entity.Models;
using Website.Shared.Entities;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class TeacherManager : BaseManager<Teacher, TeacherInputModel, TeacherOutputModel, int>, ITeacherManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public TeacherManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public override async Task<(int statusCode, string message, TeacherOutputModel output)> CreateAsync(TeacherInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
            }
            return await base.CreateAsync(input, userId);
        }

        public override async Task<(int statusCode, string message, TeacherOutputModel output)> UpdateAsync(int id, TeacherInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
            }
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage, int userId)
        {
            var entity = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message)> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage, int userId)
        {
            var entity = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            entity.IsDisplayTeacherPage = isDisplayTeacherPage;
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}
