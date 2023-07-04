using Website.Bal.Bases.Interfaces;
using Website.Shared.Entities;
using Website.Shared.Models;

namespace Website.Bal.Interfaces
{
    public interface IGalleryManager : IBaseManager<Gallery, GalleryInputModel, GalleryOutputModel, int>
    {
    }
}
