using System.ComponentModel.DataAnnotations;
using Website.Api.Filters;
using Website.Entity.Models;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Biz.Managers;
using Website.Shared.Bases.Dtos;
using Website.Shared.Common;
using Website.Shared.Dtos;

namespace Website.Api.Controllers
{
    [Route("api/parent")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class ParentController : ControllerBase
    {
        private readonly IParentManager _parentManager;
        private readonly ILogger<ParentController> _logger;

        public ParentController(
            IParentManager parentManager,
            ILogger<ParentController> logger
        ) 
        {
            _logger = logger;
            _parentManager = parentManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                (int statusCode, string message, var output) = await _parentManager.GetByIdAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ParentOutputDto>());
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
                return Ok(await _parentManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ParentInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _parentManager.CreateAsync(input.JsonMapTo<ParentInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ParentOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ParentInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _parentManager.UpdateAsync(id, input.JsonMapTo<ParentInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<ParentOutputDto>());
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
                (int statusCode, string message) = await _parentManager.DeleteAsync(id);
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
                (int statusCode, string message) = await _parentManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage);
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
