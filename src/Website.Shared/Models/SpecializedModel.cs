using System.ComponentModel.DataAnnotations;
using Website.Shared.Bases.Models;

namespace Website.Entity.Model
{
    public class SpecializedModel : BaseModel<int>
    {
        public string Name { get; set; }
    }

    public class SpecializedInputModel : SpecializedModel
    {

    }

    public class SpecializedOutputModel : SpecializedModel
    {

    }
}
