using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Managers;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Entity.Repositories;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class PostManager : BaseManager<Post, PostInputModel, PostOutputModel, int>, IPostManager
    {
        private readonly IPostRepository _postRepository;
        private readonly IBaseRepository<Post, int> _baseRepository;
        private readonly IFileManager _fileManager;
        public PostManager(
            IFileManager fileManager,
            IBaseRepository<Post, int> baseRepository,
            IPostRepository postRepository,
            IMapper mapper
        ) : base(baseRepository, mapper)
        {
            _postRepository = postRepository;   
            _baseRepository = baseRepository;
            _fileManager = fileManager;
        }
        public override async Task<(int statusCode, string message, PostOutputModel output)> CreateAsync(PostInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Post);
            }
            input.Content = _fileManager.BuildFileContent(input.Content, Folder.Post);
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _baseRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new PostOutputModel(entity));
        }

        public override async Task<(int statusCode, string message, PostOutputModel output)> UpdateAsync(int id, PostInputModel input, int userId)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntitEntityId cannot found", null);
            }
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Post);
            }

            input.Content = _fileManager.BuildFileContent(input.Content, Folder.Post);
            entity = input.MapToEntity(entity);
            entity.SetModifyDefault(userId);

            await _baseRepository.UpdateAsync(entity);

            return (StatusCodes.Status200OK, nameof(Message.Success), new PostOutputModel(entity));
        }

        public async Task<(int statusCode, string message, PostOutputModel output)> GetByPermalinkAsync(string permalink)
        {
            var query = await _postRepository.GetByPermalinkAsync(permalink);
            if (query == null)
            {
                return (StatusCodes.Status404NotFound, $"Postpermalink {permalink} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new PostOutputModel(query));
        }

        public override async Task<BasePaginationOutputModel<PostOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _baseRepository.GetListAsync(input);
            return new BasePaginationOutputModel<PostOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new PostOutputModel(s)).ToList()
            };
        }
    }
}
