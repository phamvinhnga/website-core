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

        //public async Task<bool> SetIsDisplayIndexPageAsync(int id, bool isDisplayIndexPage)
        //{
        //    var query = await _parentRepository.GetByIdAsync(id);
        //    if(query == null)
        //    {
        //        throw new BadRequestException($"Cannot find ParentId {id}");
        //    }
        //    query.IsDisplayIndexPage = isDisplayIndexPage;
        //    return await _parentRepository.UpdateAsync(query) > 0;
        //}
  
    }
}
