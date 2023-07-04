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
        public Facility MapToEntity()
        {
            return new Facility
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null,
                Index = Index,
                IsDisplayIndexPage = IsDisplayIndexPage
            };
        }

        public Facility MapToEntity(Facility entity)
        {
            entity.Name = Name;
            entity.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            entity.Description = Description;
            entity.Index = Index;
            entity.IsDisplayIndexPage = IsDisplayIndexPage;
            return entity;
        }
    }

    public class FacilityOutputModel : FacilityModel
    {
        public FacilityOutputModel(Facility entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Thumbnail = Thumbnail != null ? entity.Thumbnail.ConvertFromJson<FileModel>() : null;
            Index = entity.Index;
            Description = entity.Description;
            IsDisplayIndexPage = entity.IsDisplayIndexPage;
        }
    }
}
