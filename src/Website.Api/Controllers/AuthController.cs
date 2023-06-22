using AutoMapper;
using Website.Biz.Dto;
using Website.Biz.Managers.Interfaces;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Website.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController(
            IMapper mapper,
            IAuthManager authManager
        )
        {
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUserByIdAsync()
        {
            var userId = User.Claims.GetUserId();
            var user = await _authManager.GetCurrentUserByIdAsync(userId);
            return Ok(_mapper.Map<CurrentUserOutputDto>(user));
        }

        [HttpPut("password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] UserChangePasswordInputDto input)
        {
            return Ok(true);
        }
        
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAsync([FromBody] UserSignUpInputDto input)
        {
            var user = _mapper.Map<UserSignUpInputModel>(input);
            var result = await _authManager.SignUpAsync(user);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] UserSignInInputDto input)
        {
            var result = await _authManager.SignInAsync(
                new UserSignInInputModel 
                { 
                    UserName = input.UserName, 
                    Password = input.Password 
                }
            );
            return Ok(result);
        }

        [HttpGet("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Headers["refresh-token"].ToString();
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest( new { message = "Refresh token is empty" });
            }
            var result = await _authManager.RefreshTokenAsync(refreshToken);
            return Ok(result);
        }
    }
}
