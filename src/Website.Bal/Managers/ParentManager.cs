using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Interfaces;
using Website.Dal.UnitOfWorks;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class ParentManager : BaseManager<Parent, ParentInputModel, ParentOutputModel, int>, IParentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public ParentManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public override async Task<(int statusCode, string message, ParentOutputModel output)> CreateAsync(ParentInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            return await base.CreateAsync(input, userId);
        }

        public override async Task<(int statusCode, string message, ParentOutputModel output)> UpdateAsync(int id, ParentInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Parent);
            }
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _unitOfWork.ParentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _unitOfWork.CompleteAsync();
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
    }
}
