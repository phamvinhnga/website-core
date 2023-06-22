using Website.Entity.Entities;
using Website.Entity.Model;

namespace Website.Entity.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> CreateAsync(Post input);
        Task UpdateAsync(Post input);
        Task<Post> GetByIdAsync(int id);
        Task<Post> GetByPermalinkAsync(string permalink);
        Task DeleteAsync(Post input);
        Task<BasePageOutputModel<Post>> GetListAsync(BasePageInputModel input);
    }
}
