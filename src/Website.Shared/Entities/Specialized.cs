using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Specialized")]
    public class Specialized : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; }
    }
}
