using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileManager _fileManager;
        public PostManager(
            IFileManager fileManager,
            IPostRepository postRepository
        )
        {
            _postRepository = postRepository;   
            _fileManager = fileManager;
        }

        public virtual async Task<(int statusCode, string message, PostOutputModel output)> GetByIdAsync(int id)
        {
            var entity = await _postRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), entity.JsonMapTo<PostOutputModel>());
        }

        public virtual async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _postRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"EntityId {id} cannot found");
            }
            await _postRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message, PostOutputModel output)> CreateAsync(PostInputModel input, int userId)
        {
            if (input.Thumbnail != null && string.IsNullOrEmpty(input.Thumbnail.Id))
            {
                input.Thumbnail = _fileManager.Upload(input.Thumbnail, Folder.Post);
            }
            input.Content = _fileManager.BuildFileContent(input.Content, Folder.Post);
            var entity = input.MapToEntity();
            entity.SetCreateDefault(userId);
            await _postRepository.CreateAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success), new PostOutputModel(entity));
        }

        public async Task<(int statusCode, string message, PostOutputModel output)> UpdateAsync(int id, PostInputModel input, int userId)
        {
            var entity = await _postRepository.GetByIdAsync(id);
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

            await _postRepository.UpdateAsync(entity);

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

        public async Task<BasePaginationOutputModel<PostOutputModel>> GetListAsync(BasePaginationInputModel input)
        {
            var data = await _postRepository.GetListAsync(input);
            return new BasePaginationOutputModel<PostOutputModel>()
            {
                TotalCount = data.TotalCount,
                Items = data.Items.Select(s => new PostOutputModel(s)).ToList()
            };
        }
    }
}
