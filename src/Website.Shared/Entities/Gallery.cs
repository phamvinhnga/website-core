using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Gallery")]
    public class Gallery : BaseEntity<int>
    {
        public string Name { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
