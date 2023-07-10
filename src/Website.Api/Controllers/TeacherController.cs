using System.ComponentModel.DataAnnotations;
using Website.Api.Filters;
using Website.Entity.Models;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.Extensions.Options;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Biz.Managers;
using Website.Shared.Bases.Dtos;
using Website.Shared.Dtos;
using Website.Shared.Common;

namespace Website.Api.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherManager _teacherManager;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(
            ITeacherManager teacherManager,
            ILogger<TeacherController> logger
        ) 
        {
            _teacherManager = teacherManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.GetByIdAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TeacherOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("pagination")]
        public async Task<IActionResult> GetListAsync([FromBody] BasePaginationInputDto input)
        {
            try
            {
                return Ok(await _teacherManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TeacherInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.CreateAsync(input.JsonMapTo<TeacherInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TeacherOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TeacherInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.UpdateAsync(id, input.JsonMapTo<TeacherInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TeacherOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                (int statusCode, string message) = await _teacherManager.DeleteAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("index-page/{id:int}")]
        public async Task<IActionResult> SetIsDisplayIndexPageAsync([Required] int id, [Required] bool isDisplayIndexPage)
        {
            try
            {
                (int statusCode, string message) = await _teacherManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage, User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        
        [HttpPut("teacher-page/{id:int}")]
        public async Task<IActionResult> SetIsDisplayTeacherPageAsync([Required] int id, [Required] bool isDisplayTeacherPage)
        {
            try
            {
                (int statusCode, string message) = await _teacherManager.SetIsDisplayTeacherPageAsync(id, isDisplayTeacherPage, User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
