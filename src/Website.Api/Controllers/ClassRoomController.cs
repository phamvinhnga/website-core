using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Website.Api.Filters;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Dtos;
using Website.Shared.Bases.Models;
using Website.Shared.Common;
using Website.Shared.Dtos;
using Website.Shared.Extensions;
using Website.Shared.Models;

namespace Website.Api.Controllers
{
    [Route("api/class-room")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomManager _classRoomManager;
        private readonly ILogger<ClassRoomController> _logger;

        public ClassRoomController(
            IClassRoomManager ClassRoomManager,
            ILogger<ClassRoomController> logger
        )
        {
            _classRoomManager = ClassRoomManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                (int statusCode, string message, var output) = await _classRoomManager.GetByIdAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ClassRoomOutputDto>());
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
                return Ok(await _classRoomManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("index-page/{id}")]
        public async Task<IActionResult> SetIsDisplayIndexPageAsync([Required] int id, bool isDisplayIndexPage)
        {
            try
            {
                (int statusCode, string message) = await _classRoomManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage, User.Claims.GetUserId());
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

        [HttpPut("class-room-page/{id}")]
        public async Task<IActionResult> SetIsDisplayClassRoomPageAsync([Required] int id, bool isDisplayClassRoomPage)
        {
            try
            {
                (int statusCode, string message) = await _classRoomManager.SetIsDisplayClassRoomPageAsync(id, isDisplayClassRoomPage, User.Claims.GetUserId());
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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClassRoomInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _classRoomManager.CreateAsync(input.JsonMapTo<ClassRoomInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ClassRoomOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClassRoomInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _classRoomManager.UpdateAsync(id, input.JsonMapTo<ClassRoomInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ClassRoomOutputDto>());
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
                (int statusCode, string message) = await _classRoomManager.DeleteAsync(id);
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
