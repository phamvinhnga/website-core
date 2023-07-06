using Microsoft.AspNetCore.Mvc;
using Website.Bal.Bases.Interfaces;
using Website.Shared.Bases.Dtos;
using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Models;
using Website.Shared.Common;
using Website.Shared.Extensions;

namespace Website.Api.Base.Controllers
{
    [ApiController]
    public abstract class CrudController<TController, TEntity, TInputDto, TOutputDto, TInputModel, TOutputModel, TPrimaryKey> : ControllerBase
        where TController : class
        where TEntity : BaseEntity<TPrimaryKey>
        where TInputDto : class
        where TOutputDto : class
        where TInputModel : class
        where TOutputModel : class
        where TPrimaryKey : struct
    {
        private readonly IBaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey> _baseManager;
        private readonly ILogger<TController> _logger;

        protected CrudController(
            IBaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey> baseManager,
            ILogger<TController> logger
        )
        {
            _logger = logger;
            _baseManager = baseManager;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(TPrimaryKey id)
        {
            try
            {
                (int statusCode, string message, var output) = await _baseManager.GetByIdAsync(id);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("pagination")]
        public virtual async Task<IActionResult> GetListAsync([FromQuery] BasePaginationInputDto input)
        {
            try
            {
                return Ok(await _baseManager.GetListAsync(input.JsonMapTo<BasePaginationInputModel>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] TInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _baseManager.CreateAsync(input.JsonMapTo<TInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TOutputModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(TPrimaryKey id, [FromBody] TInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _baseManager.UpdateAsync(id, input.JsonMapTo<TInputModel>(), User.Claims.GetUserId());
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(CoreEnum.Message.MessageError.GetEnumDescription(), message);
                    return StatusCode(statusCode, new { message = message });
                }
                return Ok(output.JsonMapTo<TOutputModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(TPrimaryKey id)
        {
            try
            {
                (int statusCode, string message) = await _baseManager.DeleteAsync(id);
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
