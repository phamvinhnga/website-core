using Website.Shared.Bases.Models;

namespace Website.Shared.Models
{
    public class GalleryModel : BaseModel<int>
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
    }

    public class GalleryInputModel : GalleryModel
    {
        public GalleryInputModel() { }
    }

    public class GalleryOutputModel : GalleryModel
    {
        public GalleryOutputModel() { }
    }
}
