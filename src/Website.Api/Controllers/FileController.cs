using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Website.Entity.Model;

namespace Website.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileUploadSettingOptions _fileUploadOptions;

        public FileController(
            IOptionsMonitor<FileUploadSettingOptions> fileUploadOptions
        )
        {
            _fileUploadOptions = fileUploadOptions.CurrentValue;
        }

        [HttpGet("{folder}/{id}")]
        public IActionResult GetFileAsync([Required] string folder, [Required] string id)
        {
            var path = $"{_fileUploadOptions.Path}/{folder}/{id}";
            Console.WriteLine(path);
            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            }
            return NotFound();
        }
    }
}
