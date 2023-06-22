using AutoMapper;
using Website.Biz.Managers.Interfaces;
using Website.Entity.Entities;
using Website.Entity.Model;
using Website.Entity.Repositories.Interfaces;
using Website.Shared.Exceptions;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class ParentManager : IParentManager
    {
        private readonly IParentRepository _parentRepository;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public ParentManager(
            IParentRepository parentRepository,
            IFileManager fileManager,
            IMapper mapper
        ) {
            _parentRepository = parentRepository;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(ParentInputModel input, int userId)
        {
            var entity = _mapper.Map<Parent>(input);
            entity.SetCreateDefault(userId);
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                var file = _fileManager.Upload(input.Thumbnail, Folder.Parent);
                entity.Thumbnail = file.ConvertToJson();
            }
            else
            {
                entity.Thumbnail = null;
            }
            await _parentRepository.CreateAsync(entity);
        }

        public async Task UpdateAsync(ParentInputModel input, int userId)
        {
            var entity = await _parentRepository.GetByIdAsync(input.Id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find ParentId {input.Id}");
            }
            _mapper.Map(input, entity);
            entity.SetModifyDefault(userId);
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                var file = _fileManager.Upload(input.Thumbnail, Folder.Parent);
                entity.Thumbnail = file.ConvertToJson();
            }
            else if(input.Thumbnail == null)
            {
                entity.Thumbnail = null;
            }
            await _parentRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _parentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find ParentId {id}");
            }
            await _parentRepository.DeleteAsync(entity);
        }

        public async Task<ParentOutputModel> GetByIdAsync(int id)
        {
            var query = await _parentRepository.GetByIdAsync(id);
            if(query == null)
            {
                throw new BadRequestException($"Cannot find ParentId {id}");
            }
            return _mapper.Map<ParentOutputModel>(query);
        }

        public async Task<bool> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        {
            var query = await _parentRepository.GetByIdAsync(id);
            if(query == null)
            {
                throw new BadRequestException($"Cannot find ParentId {id}");
            }
            query.IsDisplayIndexPage = isDisplayIndexPage;
            return await _parentRepository.UpdateAsync(query) > 0;
        }
        
        public async Task<BasePageOutputModel<ParentOutputModel>> GetListAsync(BasePageInputModel input)
        {
            var query = await _parentRepository.GetListAsync(input);
            return new BasePageOutputModel<ParentOutputModel>(query.TotalItem, _mapper.Map<List<ParentOutputModel>>(query.Items));
        }
    }
}
