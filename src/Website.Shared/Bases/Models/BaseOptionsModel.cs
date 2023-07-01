using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Website.Shared.Common.CoreEnum;

namespace Website.Shared.Bases.Models
{
    public class JWTSettingOptions
    {
        public static string Position => "JWT";

        [Required]
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int Expires { get; set; }
        public int ExpiresRefreshToken { get; set; }
    }

    public class FileUploadSettingOptions
    {
        public static string Position => "Upload";

        [Required]
        public string Folder { get; set; }
        [Required]
        public string Url { get; set; }

        public string Path => $"{Folder}";

        public string SetFullUrl(string folder, string id)
        {
            return string.Format(Url, folder, id);
        }
    }

    public class DbContextConnectionSettingOptions
    {
        public static string Position => "ConnectionString";

        [Required]
        public string DefaultConnection { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Database { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string Version { get; set; }
        public string ConnectionString =>
            string.Format(DefaultConnection,
                Server,
                Database,
                UserId,
                Password,
                Port);
    }
}
