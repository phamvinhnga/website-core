using Website.Shared.Bases.Models;
using Website.Shared.Bases.Repository;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post, int>
    {
        Task<Post> GetByPermalinkAsync(string permalink);
        Task<BasePageOutputModel<Post>> GetListAsync(BasePageInputModel input);
    }
}
