using Website.Entity.Model;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;

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
            return new ClassRoom();
        }

        public ClassRoom MapToEntity(ClassRoom)
        {
            return new ClassRoom();
        }
    }

    public class ClassRoomOutputModel : ClassRoomModel
    {
        public ClassRoomOutputModel(ClassRoom entity)
        {

        }
    }
}
