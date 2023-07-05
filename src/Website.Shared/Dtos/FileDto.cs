using Website.Shared.Bases.Attributes;

namespace Website.Shared.Dtos
{
    public class FileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [ImageBase64(MaxSizeMb = 50)]
        public string Url { get; set; }
    }
}
