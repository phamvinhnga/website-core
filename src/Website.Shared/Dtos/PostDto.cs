using System;
using System.ComponentModel.DataAnnotations;
using Website.Entity.Model;

namespace Website.Shared.Dtos
{
    public class PostInputDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Permalink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public FileModel Thumbnail { get; set; }
    }

    public class PostOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public FileModel Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string Permalink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int CreateUser { get; set; }
    }
}
