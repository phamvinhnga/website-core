using System.ComponentModel.DataAnnotations.Schema;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("Category")]
    public class Category : BaseTreeEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
