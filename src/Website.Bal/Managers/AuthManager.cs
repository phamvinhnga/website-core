using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;
using Website.Shared.Models;
using static Website.Shared.Common.CoreEnum;

namespace Website.Biz.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JWTSettingOptions _jwtSettingOptions;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOptionsMonitor<JWTSettingOptions> jwtSettingOptions,
            ILogger<AuthManager> logger
        ) { 
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _jwtSettingOptions = jwtSettingOptions.CurrentValue;
        }

        public async Task<(int statusCode, string message)> ResetPasswordAsync(UserChangePasswordInputModel input, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (StatusCodes.Status404NotFound, $"UserId {userId} cannot found in system");
            }
            if (!await _userManager.CheckPasswordAsync(user, input.NewPassword))
            {
                return (StatusCodes.Status406NotAcceptable, "Mật khẩu cũ không đúng");
            }
            user.SetPasswordHasher(input.NewPassword);

            return (StatusCodes.Status200OK, nameof(Message.Success));
        }
        
        public async Task<(int statusCode, string message, UserSignInOutputModel output)> RefreshTokenAsync(string refreshToken)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(f => ClaimTypes.NameIdentifier == f.Type)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return (StatusCodes.Status400BadRequest, $"Token error", null);
            }
            var user = await _userManager.FindByIdAsync(userId);
            return (StatusCodes.Status200OK, nameof(Message.Success), await BuildTokenAsync(user));
        }

        public async Task<(int statusCode, string message, CurrentUserOutputModel output)> GetCurrentUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user == null)
            {
                return (StatusCodes.Status404NotFound, $"UserId {userId} cannot found in system", null);
            }
            return (StatusCodes.Status200OK, nameof(Message.Success), user.JsonMapTo<CurrentUserOutputModel>());
        }

        public async Task<(int statusCode, string message, CurrentUserOutputModel output)> SignUpAsync(UserSignUpInputModel input)
        {
            var entity = await _userManager.FindByEmailAsync(input.Email);
            if (entity != null)
            {
                return (StatusCodes.Status409Conflict, $"Account already exists", null);
            }
            var user = input.MapToUserEntity();
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, RoleExtension.Admin);
            return (StatusCodes.Status200OK, nameof(Message.Success), user.JsonMapTo<CurrentUserOutputModel>());
        }

        public async Task<(int statusCode, string message, UserSignInOutputModel output)> SignInAsync(UserSignInInputModel input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                return (StatusCodes.Status404NotFound, $"Username {input.UserName} cannot found in system", null);
            }

            if (await _userManager.CheckPasswordAsync(user, input.Password))
            {
                return (StatusCodes.Status200OK, nameof(Message.Success), await BuildTokenAsync(user)); 
            }
            return (StatusCodes.Status406NotAcceptable, $"Incorrect account or password", null);
        }

        private async Task<UserSignInOutputModel> BuildTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(AuthExtension.UserExtensionId, user.ExtensionId.ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (!userRoles.Any())
            {
                _logger.LogWarning($"UserName {user.UserName} have not role");
            }

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null)
                {
                    _logger.LogWarning($"Role {userRole} cant not found");
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (Claim roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingOptions.SecurityKey));
            var expires = DateTime.Now.AddHours(_jwtSettingOptions.Expires);
            var audience = _jwtSettingOptions.ValidAudience;
            var issuer = _jwtSettingOptions.ValidIssuer;
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                audience: audience,
                issuer: issuer,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new UserSignInOutputModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = BuildRefreshToken(user.Id),
                Expire = expires
            };
        }

        private string BuildRefreshToken(int userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingOptions.SecurityKey));
            var expires = DateTime.Now.AddHours(_jwtSettingOptions.ExpiresRefreshToken);
            var audience = _jwtSettingOptions.ValidAudience;
            var issuer = _jwtSettingOptions.ValidIssuer;
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                audience: audience,
                issuer: issuer,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
