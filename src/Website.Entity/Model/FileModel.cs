using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Website.Shared.Extensions;

namespace Website.Entity.Model
{
    public class FileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }

        public string SetIdRandom()
        {
            return $"no_name_{Guid.NewGuid()}".Replace("-", "_");
        }

        public string SetId()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return SetIdRandom();
            }
            return $"{Guid.NewGuid()}_{this.Name}".Replace(" ", "_").Replace("-", "_").ConvertVietnameseToEnglish();
        }
    }
}
