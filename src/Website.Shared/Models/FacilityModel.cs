using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Shared.Models
{
    public class FacilityModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FileModel Thumbnail { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public int Index { get; set; }
    }

    public class FacilityInputModel : FacilityModel
    {
        public FacilityInputModel() { }
    }

    public class FacilityOutputModel : FacilityModel
    {
        public FacilityOutputModel() { }
    }
}
