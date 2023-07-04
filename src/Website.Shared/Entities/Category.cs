using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Category")]
    public class Category : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public int Status { get; set; }
        public ICollection<Gallery> Gallerys { get; }
    }
}
