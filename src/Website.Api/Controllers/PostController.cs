﻿using Website.Api.Filters;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Dtos;

namespace Website.Api.Controllers
{
    [Route("api/post")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostManager _postManager;
        private readonly ILogger<PostController> _logger;

        public PostController(
            IPostManager postManager,
            ILogger<PostController> logger
        ) 
        {
            _postManager = postManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                (int statusCode, string message, var output) = await _postManager.GetByIdAsync(id);
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
        public async Task<IActionResult> GetListAsync([FromQuery] BasePageInputModel input)
        {
            return Ok(await _postManager.GetListAsync(input));
        }

        [HttpPost]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] PostInputModel input)
        {
            try
            {
                (int statusCode, string message, var output) = await _postManager.CreateAsync(input, User.Claims.GetUserId());
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostInputModel input)
        {
            try
            {
                (int statusCode, string message, var output) = await _postManager.UpdateAsync(id, input, User.Claims.GetUserId());
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
                (int statusCode, string message) = await _postManager.DeleteAsync(id);
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
