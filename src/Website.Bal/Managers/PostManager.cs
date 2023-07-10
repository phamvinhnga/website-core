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
    public class PostManager : BaseManager<Post, PostInputModel, PostOutputModel, int>, IPostManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public PostManager(
            IFileManager fileManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
        ) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;   
            _fileManager = fileManager;
            _mapper = mapper;
        }

        public override async Task<(int statusCode, string message, PostOutputModel output)> CreateAsync(PostInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Post);
            }
            input.Content = _fileManager.BuildFileContent(input.Content, Folder.Post);
            return await base.CreateAsync(input, userId);
        }

        public override async Task<(int statusCode, string message, PostOutputModel output)> UpdateAsync(int id, PostInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Post);
            }
            input.Content = _fileManager.BuildFileContent(input.Content, Folder.Post);
            return await base.UpdateAsync(id, input, userId);
        }

        public async Task<(int statusCode, string message, PostOutputModel output)> GetByPermalinkAsync(string permalink)
        {
            var query = await _unitOfWork.PostRepository.GetByPermalinkAsync(permalink);
            if (query == null)
            {
                return (StatusCodes.Status404NotFound, $"Postpermalink {permalink} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), _mapper.Map<Post, PostOutputModel>(query));
        }
    }
}
