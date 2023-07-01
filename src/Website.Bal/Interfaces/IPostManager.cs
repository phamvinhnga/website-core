using Website.Entity.Model;
using Website.Shared.Bases.Models;

namespace Website.Bal.Interfaces
{
    public interface IPostManager
    {
        Task<(int statusCode, string message, PostOutputModel output)> CreateAsync(PostInputModel input, int userId);
        Task<(int statusCode, string message, PostOutputModel output)> UpdateAsync(int id, PostInputModel input, int userId);
        Task<(int statusCode, string message, PostOutputModel output)> GetByIdAsync(int id);
        Task<(int statusCode, string message, PostOutputModel output)> GetByPermalinkAsync(string permalink);
        Task<(int statusCode, string message)> DeleteAsync(int id);
        Task<BasePageOutputModel<PostOutputModel>> GetListAsync(BasePageInputModel input);
    }
}
