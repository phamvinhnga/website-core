using Website.Shared.Bases.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post, int>
    {
        Task<Post> GetByPermalinkAsync(string permalink);
    }
}
