using Website.Shared.Bases.Interfaces;
using static Website.Shared.Bases.Enums.BaseEnum;

namespace Website.Shared.Bases.Models
{
    public partial class BaseCriteriaRequestModel : IBaseCriteriaRequest
    {
        public string Property { get; set; }

        public OptionCriteriaRequest Option { get; set; }

        public string Value { get; set; }
    }
}
