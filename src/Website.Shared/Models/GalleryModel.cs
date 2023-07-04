using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;

namespace Website.Shared.Models
{
    public class GalleryModel : BaseModel<int>
    {
        public string Name { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
    }

    public class GalleryInputModel : GalleryModel
    {
        public GalleryInputModel() { }

        public Gallery MapToEntity()
        {
            return new Gallery { };
        }

        public Gallery MapToEntity(Gallery entity)
        {
            return new Gallery { };
        }
    }

    public class GalleryOutputModel : GalleryModel
    {
        public GalleryOutputModel(Gallery entity) { }
    }
}
