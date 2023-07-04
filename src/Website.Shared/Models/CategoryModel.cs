using System.ComponentModel.DataAnnotations;
using Website.Shared.Bases.Models;

namespace Website.Shared.Models
{
    public class CategoryModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }
        public int Status { get; set; }
    }

    public class CategoryInputModel : CategoryModel
    {
        public CategoryInputModel() { }
    }

    public class CategoryOutputModel : CategoryModel
    {
        public CategoryOutputModel() { }
    }
}
