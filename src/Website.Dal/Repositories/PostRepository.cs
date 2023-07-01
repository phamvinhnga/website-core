using Microsoft.EntityFrameworkCore;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Repository;
using Website.Shared.Entities;

namespace Website.Entity.Repositories
{
    public class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context) { }

        public Task<Post> GetByPermalinkAsync(string permalink)
        {
            return Queryable.AsNoTracking().FirstOrDefaultAsync(f => f.Permalink == permalink);
        }
    }
}
