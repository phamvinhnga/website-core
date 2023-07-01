using static Website.Shared.Bases.Enums.BaseEnum;

namespace Website.Shared.Bases.Interfaces
{
    public interface IBaseCriteriaRequest
    {
        string Property { get; set; }

        OptionCriteriaRequest Option { get; set; }

        string Value { get; set; }
    }
}
