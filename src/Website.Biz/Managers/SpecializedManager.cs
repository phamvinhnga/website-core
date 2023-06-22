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
    public class SpecializedManager : ISpecializedManager
    {
        private readonly ISpecializedRepository _specializedRepository;
        private readonly IMapper _mapper;

        public SpecializedManager(
            ISpecializedRepository specializedRepository,
            IMapper mapper
        )
        {
            _specializedRepository = specializedRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(SpecializedInputModel input, int userId)
        {
            var entity = _mapper.Map<Specialized>(input);
            entity.SetCreateDefault(userId);
            await _specializedRepository.CreateAsync(entity);
        }

        public async Task UpdateAsync(SpecializedInputModel input, int userId)
        {
            var entity = await _specializedRepository.GetByIdAsync(input.Id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find SpecializedId {input.Id}");
            }
            _mapper.Map(input, entity);
            entity.SetModifyDefault(userId);
            await _specializedRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _specializedRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new BadRequestException($"Cannot find SpecializedId {id}");
            }
            if(await _specializedRepository.CountNumberTeacherBySpecializedId(id) > 0)
            {
                throw new BadRequestException($"Cần phải đổi chuyên ngành của toàn bộ giáo viên {entity.Name} trước khi xóa");
            }
            await _specializedRepository.DeleteAsync(entity);
        }

        public async Task<SpecializedOutputModel> GetByIdAsync(int id)
        {
            var query = await _specializedRepository.GetByIdAsync(id);
            if (query == null)
            {
                throw new BadRequestException($"Cannot find SpecializedId {id}");
            }
            return _mapper.Map<SpecializedOutputModel>(query);
        }

        public async Task<BasePageOutputModel<SpecializedOutputModel>> GetListAsync(BasePageInputModel input)
        {
            var query = await _specializedRepository.GetListAsync(input);
            return new BasePageOutputModel<SpecializedOutputModel>(query.TotalItem, _mapper.Map<List<SpecializedOutputModel>>(query.Items));
        }
    }
}
