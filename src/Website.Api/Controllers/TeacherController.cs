using System.ComponentModel.DataAnnotations;
using Website.Api.Filters;
using Website.Entity.Model;
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
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.GetByIdAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<CurrentUserOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> GetListAsync([FromQuery] BasePaginationInputDto input)
        {
            try
            {
                return Ok(await _teacherManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] TeacherInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.CreateAsync(input, User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<CurrentUserOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TeacherInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _teacherManager.UpdateAsync(id, input, User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<CurrentUserOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                (int statusCode, string message) = await _teacherManager.DeleteAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("index-page/{id:int}")]
        public async Task<IActionResult> SetIsDisplayIndexPageAsync([Required] int id, [Required] bool isDisplayIndexPage)
        {
            return Ok(await _teacherManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage));
        }
        
        [HttpPut("teacher-page/{id:int}")]
        public async Task<IActionResult> SetIsDisplayTeacherPageAsync([Required] int id, [Required] bool isDisplayTeacherPage)
        {
            return Ok(await _teacherManager.SetIsDisplayTeacherPageAsync(id, isDisplayTeacherPage));
        }
    }
}
