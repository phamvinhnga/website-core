using System.ComponentModel.DataAnnotations;


namespace Website.Domain.Dtos
{
    public class PostInputDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string Thumbnail { get; set; }
        public string Images { get; set; }
    }
}
