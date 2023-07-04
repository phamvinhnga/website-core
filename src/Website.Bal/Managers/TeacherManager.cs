﻿using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class TeacherManager : ITeacherManager
    {
        private readonly ITeacherRepository _parentRepository;
        private readonly IFileManager _fileManager;

        public TeacherManager(
            IFileManager fileManager,
            ITeacherRepository parentRepository
        ) 
        {
            _parentRepository = parentRepository;
            _fileManager = fileManager;
        }

        public async Task<(int statusCode, string message, TeacherOutputModel output)> CreateAsync(TeacherInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Teacher);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _parentRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<(int statusCode, string message, TeacherOutputModel output)> UpdateAsync(int id, TeacherInputModel input, int userId)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
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
   
            await _parentRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found");
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _parentRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message)> SetIsDisplayTeacherPageAsync(int id, bool isDisplayTeacherPage)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found");
            }
            entity.IsDisplayTeacherPage = isDisplayTeacherPage;
            await _parentRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message, TeacherOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"TeacherId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new TeacherOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<TeacherOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _parentRepository.GetListAsync(input);
            return new BasePaginationOutputModel<TeacherOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new TeacherOutputModel(s)).ToList()
            };
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            await _parentRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}
