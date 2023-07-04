using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Gallery")]
    public class Gallery : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public bool IsDisplayGalleryPage { get; set; }
    }
}
