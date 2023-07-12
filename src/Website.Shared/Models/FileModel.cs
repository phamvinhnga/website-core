using System;
using System.IO;
using System.Text.RegularExpressions;
using Website.Shared.Extensions;

namespace Website.Entity.Models
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
            return ReplaceSpecialCharactersFileName(this.Name);
        }

        private static string ReplaceSpecialCharactersFileName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).ConvertVietnameseToEnglish();
            var strRamndom = Guid.NewGuid().ToString().Substring(0, 8);

            var replacedFileNameWithoutExtension = Regex.Replace(fileNameWithoutExtension, @"[^a-zA-Z0-9]+", " ");
            var words = replacedFileNameWithoutExtension.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return $"{string.Join("_", words)}_{strRamndom}{fileExtension}";
        }
    }
}
