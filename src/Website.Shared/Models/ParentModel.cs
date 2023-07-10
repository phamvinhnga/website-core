using System;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Entity.Models
{
    public class ParentInputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }

    public class ParentOutputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileModel Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }        
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }
}
