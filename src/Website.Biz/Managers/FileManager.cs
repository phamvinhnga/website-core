using Website.Biz.Managers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using static Website.Shared.Common.CoreEnum;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.Extensions.Options;

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
            foreach (Match item in matches)
            {
                var path = _fileUploadOptions.Path;
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadOptions.Path);
                {
                    path += $"/{folder}";
                    uploadPath = Path.Combine(uploadPath, folder.ToString());
                }
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var base64String = item.Groups[2].Value;
                byte[] fileBytes = Convert.FromBase64String(base64String);
                var id = $"no_name_{Guid.NewGuid()}{GetTypeFile(item.Groups[0].Value)}".Replace("-", "_");
                using (var fs = new FileStream($"{uploadPath}/{id}", FileMode.Create))
                {
                    fs.Write(fileBytes, 0, fileBytes.Length);
                }
                input = input.Replace(item.Value, _fileUploadOptions.SetFullUrl(folder.ToString(), id));
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

            var path = _fileUploadOptions.Path;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadOptions.Path);
            {
                path += $"/{folder}";
                uploadPath = Path.Combine(uploadPath, folder.ToString());
            }
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string str = Regex.Replace(file.Url, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            byte[] fileBytes = Convert.FromBase64String(str);
            using (var fs = new FileStream($"{uploadPath}/{result.Id}", FileMode.Create))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
            result.Url = string.Format(_fileUploadOptions.Url, folder, result.Id);
            return result;
        }

        private string GetTypeFile(string base64String)
        {
            if (base64String.Contains("data:image/png;base64,"))
            {
                return ".png";
            }
            else if (base64String.Contains("data:image/jpeg;base64,"))
            {
                return ".jpg";
            }
            else if (base64String.Contains("data:image/jpg;base64,"))
            {
                return ".jpg";
            }
            else if (base64String.Contains("data:image/gif;base64,"))
            {
                return ".gif";
            }
            return string.Empty;
        }
    }
}
