using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    public class WebSetting : BaseEntity<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
