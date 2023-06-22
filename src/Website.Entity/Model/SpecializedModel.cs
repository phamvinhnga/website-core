using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Entity.Entities;

namespace Website.Entity.Model
{
    public class SpecializedModel : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }

    public class SpecializedInputModel : SpecializedModel
    {

    }

    public class SpecializedOutputModel : SpecializedModel
    {

    }
}
