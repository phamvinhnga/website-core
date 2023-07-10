using Website.Entity.Models;

namespace Website.Shared.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
        public bool IsDisplayGalleryPage { get; set; }
    }

    public class GalleryInputModel : GalleryModel
    {
        public GalleryInputModel() { }
    }

    public class GalleryOutputModel : GalleryModel
    {
        public GalleryOutputModel() 
        {
        }
    }
}
