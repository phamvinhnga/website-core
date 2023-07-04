using Website.Bal.Bases.Interfaces;
using Website.Entity.Model;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Bal.Interfaces
{
    public interface IPostManager : IBaseManager<Post, PostInputModel, PostOutputModel, int>
    {
        Task<(int statusCode, string message, PostOutputModel output)> GetByPermalinkAsync(string permalink);
    }
}
