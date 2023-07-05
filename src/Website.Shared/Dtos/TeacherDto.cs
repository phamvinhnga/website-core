using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Dtos
{
    public class TeacherInputDto
    {
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        [Required]
        public int SpecializedId { get; set; }
        public FileImgeDto Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }
    }

    public class TeacherOutputDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int SpecializedId { get; set; }
        public FileImgeDto Thumbnail { get; set; }
        public SpecializedDto Specialized { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }
    }
}
