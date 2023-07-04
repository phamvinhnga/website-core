using Website.Entity.Models;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Shared.Models
{
    public class ClassRoomModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int TotalSeats { get; set; }
        public FileModel Thumbnail { get; set; }
        public decimal TutionFee { get; set; }
        public string TutionType { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayClassRoomPage { get; set; }
        public bool Status { get; set; }
    }

    public class ClassRoomInputModel : ClassRoomModel
    {
        public ClassRoom MapToEntity()
        {
            return new ClassRoom()
            {
                Name = Name,
                Description = Description,
                FromAge = FromAge,
                ToAge = ToAge,
                TotalSeats = TotalSeats,
                Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null,
                TutionFee = TutionFee,
                TutionType = TutionType,
                FromTime = FromTime,
                ToTime = ToTime,
                Index = Index,
                IsDisplayIndexPage = IsDisplayIndexPage,
                IsDisplayClassRoomPage = IsDisplayClassRoomPage,
                Status = Status
            };
        }

        public ClassRoom MapToEntity(ClassRoom entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.FromAge = FromAge;
            entity.ToAge = ToAge;
            entity.TotalSeats = TotalSeats;
            entity.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            entity.TutionFee = TutionFee;
            entity.TutionType = TutionType;
            entity.FromTime = FromTime;
            entity.ToTime = ToTime;
            entity.Index = Index;
            entity.IsDisplayIndexPage = IsDisplayIndexPage;
            entity.IsDisplayClassRoomPage = IsDisplayClassRoomPage;
            entity.Status = Status;
            return entity;
        }
    }

    public class ClassRoomOutputModel : ClassRoomModel
    {
        public ClassRoomOutputModel(ClassRoom entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            FromAge = entity.FromAge;
            ToAge = entity.ToAge;
            TotalSeats = entity.TotalSeats;
            Thumbnail = entity.Thumbnail.ConvertFromJson<FileModel>();
            TutionFee = entity.TutionFee;
            TutionType = entity.TutionType;
            FromTime = entity.FromTime;
            ToTime = entity.ToTime;
            Index = entity.Index;
            IsDisplayIndexPage = entity.IsDisplayIndexPage;
            IsDisplayClassRoomPage = entity.IsDisplayClassRoomPage;
            Status = entity.Status;
        }
    }
}
