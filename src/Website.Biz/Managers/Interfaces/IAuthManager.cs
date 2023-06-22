using Website.Entity.Model;
using Microsoft.AspNetCore.Identity;

namespace Website.Biz.Managers.Interfaces
{
    public interface IAuthManager
    {
        Task<UserSignInOutputModel> RefreshTokenAsync(string refreshToken);

        Task<IdentityResult> SignUpAsync(UserSignUpInputModel input);

        Task<UserSignInOutputModel> SignInAsync(UserSignInInputModel input);

        Task<CurrentUserOutputModel> GetCurrentUserByIdAsync(int userId);
    }
}
