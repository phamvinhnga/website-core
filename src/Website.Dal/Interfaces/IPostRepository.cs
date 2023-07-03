using Website.Dal.Bases.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post, int>
    {
        Task<Post> GetByPermalinkAsync(string permalink);
    }
}
