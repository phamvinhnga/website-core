using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Entity.Entities
{
    [Table("Category")]
    public class Category : BaseTreeEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
