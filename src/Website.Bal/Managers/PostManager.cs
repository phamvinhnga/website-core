using AutoMapper;
using Microsoft.AspNetCore.Http;
using Website.Bal.Interfaces;
using Website.Dal.Interfaces;
using Website.Entity.Model;
using Website.Shared.Bases.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileManager _fileManager;

        public PostManager(
            IPostRepository postRepository,
            IFileManager fileManager,
            IMapper mapper
        ) {
            _postRepository = postRepository;
            _fileManager = fileManager;
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
                return (StatusCodes.Status404NotFound, $"PostId {id} cannot found", null);
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

        public async Task<(int statusCode, string message)> DeleteAsync(int id)
        {
            var entity = await _postRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return (StatusCodes.Status404NotFound, $"PostId {id} cannot found");
            }
            await _postRepository.DeleteAsync(entity);
            return (StatusCodes.Status200OK, nameof(Message.Success));
        }

        public async Task<(int statusCode, string message, PostOutputModel output)> GetByIdAsync(int id)
        {
            var query = await _postRepository.GetByIdAsync(id);
            if(query == null)
            {
                return (StatusCodes.Status404NotFound, $"PostId {id} cannot found", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), new PostOutputModel(query));
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

        public async Task<BasePageOutputModel<PostOutputModel>> GetListAsync(BasePageInputModel input)
        {
            var query = await _postRepository.GetListAsync(input);
            return new BasePageOutputModel<PostOutputModel>(query.TotalItem, query.Items.Select(s => new PostOutputModel(s)).ToList());
        }
    }
}
