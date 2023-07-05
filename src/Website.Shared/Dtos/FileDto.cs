using Website.Shared.Bases.Attributes;

namespace Website.Shared.Dtos
{
    public class FileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public virtual string Url { get; set; }
    }

    public class FileImgeDto : FileDto
    {
        [ImageBase64(MaxSizeMb = 50)]
        public override string Url { get; set; }
    }
}
