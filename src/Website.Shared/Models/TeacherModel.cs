
using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Entity.Model
{
    public class TeacherInputModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Surname { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int SpecializedId { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }
    }

    public class TeacherOutputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int SpecializedId { get; set; }
        public FileModel Thumbnail { get; set; }
        public SpecializedModel Specialized { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }
    }
}
