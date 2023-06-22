using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Website.Entity.Model
{
    public class PostInputModel 
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

    public class PostOutputModel
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
