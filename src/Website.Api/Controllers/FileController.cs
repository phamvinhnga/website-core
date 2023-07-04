using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Website.Shared.Bases.Models;
using Website.Shared.Common;
using Website.Shared.Extensions;

namespace Website.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileUploadSettingOptions _fileUploadOptions;
        private readonly ILogger<FileController> _logger;

        public FileController(
            ILogger<FileController> logger,
            IOptionsMonitor<FileUploadSettingOptions> fileUploadOptions
        )
        {
            _logger = logger;
            _fileUploadOptions = fileUploadOptions.CurrentValue;
        }

        [HttpGet("{folder}/{id}")]
        public IActionResult GetFileAsync([Required] string folder, [Required] string id)
        {
            try
            {
                var path = $"{_fileUploadOptions.Path}/{folder}/{id}";
                Console.WriteLine(path);
                if (System.IO.File.Exists(path))
                {
                    return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
                }
                return NotFound("Cannot found file");
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
    
        }
    }
}
