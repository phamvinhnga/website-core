using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Bal.Interfaces;
using static Website.Shared.Common.CoreEnum;
using Website.Dal.Interfaces;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Extensions;
using Website.Dal.Repositories;
using Website.Shared.Models;
using Website.Dal.Bases.Managers;
using Website.Shared.Entities;
using AutoMapper;
using Website.Dal.UnitOfWorks;

namespace Website.Bal.Managers
{
    public class FacilityManager : BaseManager<Facility, FacilityInputModel, FacilityOutputModel, int>, IFacilityManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;

        public FacilityManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public new async Task<(int statusCode, string message, FacilityOutputModel output)> CreateAsync(FacilityInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Facility);
            }
            return await base.CreateAsync(input, userId);
        }

        public new async Task<(int statusCode, string message, FacilityOutputModel output)> UpdateAsync(int id, FacilityInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Facility);
            }
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _unitOfWork.FacilityRepository.GetByIdAsync(id);
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