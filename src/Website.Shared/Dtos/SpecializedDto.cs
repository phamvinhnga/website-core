using Website.Shared.Bases.Models;

namespace Website.Shared.Dtos
{
    public class SpecializedDto : BaseModel<int>
    {
        public string Name { get; set; }
    }

    public class SpecializedInputDto : SpecializedDto
    {

    }

    public class SpecializedOutputDto : BaseModel<int>
    {

    }
}
