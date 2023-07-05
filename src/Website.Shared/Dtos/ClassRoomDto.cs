using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Dtos
{
    public class ClassRoomDto 
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int FromAge { get; set; }
        [Required]
        public int ToAge { get; set; }
        public int TotalSeats { get; set; }
        public FileImgeDto Thumbnail { get; set; }
        public decimal TutionFee { get; set; }
        public string TutionType { get; set; }
        [Required]
        [RegularExpression(@"^([01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Please use the format 'HH:mm'.")]
        public string FromTime { get; set; }
        [Required]
        [RegularExpression(@"^([01][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format. Please use the format 'HH:mm'.")]
        public string ToTime { get; set; }
        [Required]
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayClassRoomPage { get; set; }
        [Required]
        public bool Status { get; set; }
    }

    public class ClassRoomInputDto : ClassRoomDto
    {
        public ClassRoomInputDto() { }
    }

    public class ClassRoomOutputDto : ClassRoomDto
    {
        public ClassRoomOutputDto() { }
    }
}
