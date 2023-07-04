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

namespace Website.Bal.Managers
{
    public class FacilityManager : IFacilityManager
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IFileManager _fileManager;
        public FacilityManager(
            IFileManager fileManager,
            IFacilityRepository facilityRepository
        )
        {
            _facilityRepository = facilityRepository;
            _fileManager = fileManager;
        }

        public async Task<(int statusCode, string message, FacilityOutputModel output)> CreateAsync(FacilityInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Facility);
            }
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _facilityRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new FacilityOutputModel(entity));
        }

        public async Task<(int statusCode, string message, FacilityOutputModel output)> UpdateAsync(int id, FacilityInputModel input, int userId)
        {
            var entity = await _facilityRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }

            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Facility);
            }
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _facilityRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new FacilityOutputModel(entity));
        }

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _facilityRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            await _facilityRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message)> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var entity = await _facilityRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id));
            }
            entity.IsDisplayIndexPage = isDisplayIndexPage;
            await _facilityRepository.UpdateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message, FacilityOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _facilityRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, string.Format(Message.MessageEntityNotFound.GetEnumDescription(), id), null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new FacilityOutputModel(entity));
        }

        public async Task<BasePaginationOutputModel<FacilityOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _facilityRepository.GetListAsync(input);
            return new BasePaginationOutputModel<FacilityOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new FacilityOutputModel(s)).ToList()
            };
        }
    }
}