using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Facility")]
    public class Facility : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public int Index { get; set; }
    }
}
