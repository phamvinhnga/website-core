using Website.Shared.Bases.Interfaces;
using static Website.Shared.Bases.Enums.BaseEnum;

namespace Website.Shared.Bases.Dtos
{
    public partial class BaseCriteriaRequestDto : IBaseCriteriaRequest
    {
        public string Property { get; set; }

        public OptionCriteriaRequest Option { get; set; }

        public string Value { get; set; }
    }
}
