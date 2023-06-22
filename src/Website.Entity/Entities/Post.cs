using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Entity.Entities
{
    [Table("Post")]
    public class Post : BaseEntity<int>
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Thumbnail { get; set; }
        public string Images { get; set; }
        public string Permalink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        
    }
}
