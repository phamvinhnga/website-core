using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Common;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class ParentManager : IParentManager
    {
        private readonly IParentRepository _parentRepository;
        private readonly IFileManager _fileManager;
        public ParentManager(
            IFileManager fileManager,
            IParentRepository baseRepository
        )
        {
            _parentRepository = baseRepository;
            _fileManager = fileManager;
        }

        public async Task<(int statusCode, string message, ParentOutputModel output)> CreateAsync(ParentInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _parentRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public async Task<(int statusCode, string message, ParentOutputModel output)> UpdateAsync(int id, ParentInputModel input, int userId)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _parentRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            await _parentRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }


        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _parentRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message, ParentOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new ParentOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<ParentOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _parentRepository.GetListAsync(input);
            return new BasePaginationOutputModel<ParentOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new ParentOutputModel(s)).ToList()
            };
        }
    }
}
