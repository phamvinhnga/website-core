using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Website.Bal.Interfaces;
using Website.Shared.Dtos;
using Website.Shared.Models;

namespace Website.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger; 

        public AuthController(
            IAuthManager authManager,
            ILogger<AuthController> logger
        )
        {
            _logger = logger;
            _authManager = authManager;
        }

        [HttpGet("current-user")]
        [ProducesResponseType(typeof(CurrentUserOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrentUserByIdAsync()
        {
            var userId = User.Claims.GetUserId();
            try
            {
                (int statusCode, string message, var output) = await _authManager.GetCurrentUserByIdAsync(userId);
                if(statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<CurrentUserOutputDto>());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(CurrentUserOutputDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAsync([FromBody] UserSignUpInputDto input)
        {
            try
            {
                var user = input.JsonMapTo<UserSignUpInputModel>();
                (int statusCode, string message, var output) = await _authManager.SignUpAsync(user);
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

        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(UserSignInOutputDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] UserSignInInputDto input)
        {
            try
            {
                (int statusCode, string message, var output) = await _authManager.SignInAsync(new UserSignInInputModel(input.UserName, input.Password));
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<UserSignInOutputDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("refresh-token")]
        [ProducesResponseType(typeof(UserSignInOutputModel), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            try
            {
                var refreshToken = Request.Headers["refresh-token"].ToString();
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest(new { message = "Refresh token is empty" });
                }
                (int statusCode, string message, var output) = await _authManager.RefreshTokenAsync(refreshToken);
                if (statusCode != StatusCodes.Status200OK)
                {
                    _logger.LogWarning(message);
                    return StatusCode(statusCode, message);
                }
                return Ok(output.JsonMapTo<UserSignInOutputModel>());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
