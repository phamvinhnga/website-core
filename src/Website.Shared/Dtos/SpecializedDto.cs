using Website.Shared.Bases.Models;

namespace Website.Shared.Dtos
{
    public class SpecializedDto : BaseModel<int>
    {
        public string Name { get; set; }
    }

    public class SpecializedInputDto : SpecializedDto
    {
        public SpecializedInputDto() { }
    }

    public class SpecializedOutputDto : BaseModel<int>
    {
        public SpecializedOutputDto() { }
    }
}
