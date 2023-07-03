using System;

namespace Website.Shared.Dtos
{
    public class ParentInputDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileDto Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }

    public class ParentOutputDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileDto Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }
}
