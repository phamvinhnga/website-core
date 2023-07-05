using System.Text.RegularExpressions;
using static Website.Shared.Common.CoreEnum;
using Website.Entity.Models;
using Microsoft.Extensions.Options;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using HeyRed.Mime;

namespace Website.Biz.Managers
{
    public class FileManager : IFileManager
    {
        private readonly FileUploadSettingOptions _fileUploadOptions;

        public FileManager(
            IOptionsMonitor<FileUploadSettingOptions> fileUploadOptions
        ) 
        {
            _fileUploadOptions = fileUploadOptions.CurrentValue;
        }

        public string BuildFileContent(string input, Folder folder)
        {
            if (input == null)
            {
                return null;
            }

            var reg = "\"data:([^;]*);base64,([^\"]*)\"";
            var matches = Regex.Matches(input, reg, RegexOptions.IgnoreCase);

            var path = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadOptions.Path, folder.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (Match item in matches)
            {
                var base64String = item.Groups[2].Value;
                byte[] fileBytes = Convert.FromBase64String(base64String);
                var fileExtension = GetBase64ImageExtension(base64String);
                var id = $"no_name_{Guid.NewGuid()}{fileExtension}".Replace("-", "_");
                var filePath = Path.Combine(path, id);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(fileBytes, 0, fileBytes.Length);
                }

                var replacedValue = _fileUploadOptions.SetFullUrl(folder.ToString(), id);
                input = input.Replace(item.Value, replacedValue);
            }

            return input;
        }

        public FileModel Upload(FileModel file, Folder folder)
        {
            if (file == null)
            {
                return null;
            }

            var result = new FileModel();

            if (string.IsNullOrEmpty(file.Name))
            {
                result.Id = result.SetIdRandom();
                result.Name = null;
            }
            else
            {
                result.Name = file.Name;
                result.Id = result.SetId();
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadOptions.Path, folder.ToString());
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string base64Data = Regex.Replace(file.Url, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            byte[] fileBytes = Convert.FromBase64String(base64Data);
            string filePath = Path.Combine(uploadPath, result.Id);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            result.Url = _fileUploadOptions.SetFullUrl(folder.ToString(), result.Id);
            return result;
        }

        private string GetBase64ImageExtension(string base64Image)
        {
            var mimeType = MimeGuesser.GuessMimeType(base64Image);
            var extension = MimeTypesMap.GetExtension(mimeType);
            if (!string.IsNullOrEmpty(extension))
            {
                return "." + extension;
            }
            else
            {
                return ".unknown";
            }
        }
    }
}
