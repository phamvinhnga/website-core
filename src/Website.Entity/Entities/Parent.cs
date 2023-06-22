using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Entity.Entities
{
    [Table("Parent")]
    public class Parent : BaseEntity<int>
    {
        [Required]
        [StringLength(64)]
        public string Surname { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public virtual string FullName => $"{this.Surname.Trim()} {this.Name.Trim()}";
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }
}
