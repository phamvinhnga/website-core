using AutoMapper;
using Website.Biz.Managers.Interfaces;
using Website.Entity.Model;
using Website.Entity.Entities;
using Website.Shared.Exceptions;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Website.Biz.Dto;

namespace Website.Biz.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettingOptions _jwtSettingOptions;
        private readonly ILogger<AuthManager> _logger;
        private readonly IMapper _mapper;

        public AuthManager(
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOptionsMonitor<JWTSettingOptions> jwtSettingOptions,
            ILogger<AuthManager> logger
        ) { 
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _jwtSettingOptions = jwtSettingOptions.CurrentValue;
            _signInManager = signInManager;
        }

        public async Task ResetPasswordAsync(UserChangePasswordInputDto input, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new ArgumentNullException($"UserId {userId} cannot found in system");
            }

            if (!await _userManager.CheckPasswordAsync(user, input.NewPassword))
            {
                throw new BadRequestException("Mật khẩu cũ không đúng");
            }
            user.SetPasswordHasher(input.NewPassword);
            //await _userManager.save
        }
        
        public async Task<UserSignInOutputModel> RefreshTokenAsync(string refreshToken)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(f => ClaimTypes.NameIdentifier == f.Type)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new BadRequestException("Token error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            return await BuildTokenAsync(user);
        }

        public async Task<CurrentUserOutputModel> GetCurrentUserByIdAsync(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentNullException("Current User");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if(user == null)
            {
                throw new ArgumentNullException($"UserId {userId} cannot found in system");
            }

            return _mapper.Map<CurrentUserOutputModel>(user);
        }

        public async Task<IdentityResult> SignUpAsync(UserSignUpInputModel input)
        {
            var user = _mapper.Map<User>(input);

            var entity = await _userManager.FindByEmailAsync(input.Email);

            if (entity != null)
            {
                throw new BadRequestException("Account already exists", StatusCodes.Status409Conflict);
            }

            user.SetPasswordHasher(input.Password);
            var result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, RoleExtension.Staff);
            return result;
        }

        public async Task<UserSignInOutputModel> SignInAsync(UserSignInInputModel input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);

            if (user == null)
            {
                throw new ArgumentNullException($"Username {input.UserName} cannot found in system");
            }

            if (await _userManager.CheckPasswordAsync(user, input.Password))
            {
                return await BuildTokenAsync(user);
            }

            throw new UnauthorizedException("Incorrect account or password", StatusCodes.Status406NotAcceptable);
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
                    _logger.LogError($"Role {userRole} cant not found");
                    throw new ApplicationException($"Role {userRole} cant not found");
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
