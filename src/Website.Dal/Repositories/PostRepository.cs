using Microsoft.EntityFrameworkCore;
using Website.Dal;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Bases.Repository;
using Website.Shared.Entities;

namespace Website.Entity.Repositories
{
    public class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context) { }

        public async Task<BasePageOutputModel<Post>> GetListAsync(BasePageInputModel input)
        {
            var query = base.Queryable.AsNoTracking().Where(w => w.Title.StartsWith(input.Search)).Select(s => new Post()
            {
                Id = s.Id,
                Title = s.Title,
                Thumbnail = s.Thumbnail,
                CreateDate = s.CreateDate
            });

            var count = await query.CountAsync();

            var items = await query.Skip(input.SkipCount).Take(input.MaxCountResult).ToListAsync();

            return new BasePageOutputModel<Post>(count, items);
        }

        public Task<Post> GetByPermalinkAsync(string permalink)
        {
            return Queryable.AsNoTracking().FirstOrDefaultAsync(f => f.Permalink == permalink);
        }
    }
}
