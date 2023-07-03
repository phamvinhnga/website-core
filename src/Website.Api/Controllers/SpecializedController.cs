using Website.Api.Filters;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Bases.Dtos;
using Website.Shared.Dtos;

namespace Website.Api.Controllers
{
    [Route("api/specialized")]
    [ApiController]
    //[Authorize]
    //[ServiceFilter(typeof(AdminRoleFilter))]
    public class SpecializedController : ControllerBase
    {
        private readonly ISpecializedManager _specializedManager;
        private readonly ILogger<SpecializedController> _logger;

        public SpecializedController(
            ISpecializedManager specializedManager,
            ILogger<SpecializedController> logger
        ) 
        {
            _logger = logger;
            _specializedManager = specializedManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] BasePaginationInputDto input)
        {
            try
            {
                return Ok(await _specializedManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PostInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _specializedManager.CreateAsync(input.JsonMapTo<SpecializedInputModel>(), User.Claims.GetUserId());
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _specializedManager.UpdateAsync(id, input.JsonMapTo<SpecializedInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<PostOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                (int statusCode, string message) = await _specializedManager.DeleteAsync(id);
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
    }
}
