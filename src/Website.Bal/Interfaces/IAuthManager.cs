using Website.Shared.Models;

namespace Website.Bal.Interfaces
{
    public interface IAuthManager
    {
        Task<(int statusCode, string message)> ResetPasswordAsync(UserChangePasswordInputModel input, int userId);
        Task<(int statusCode, string message, UserSignInOutputModel output)> RefreshTokenAsync(string refreshToken);
        Task<(int statusCode, string message, CurrentUserOutputModel output)> SignUpAsync(UserSignUpInputModel input);
        Task<(int statusCode, string message, UserSignInOutputModel output)> SignInAsync(UserSignInInputModel input);
        Task<(int statusCode, string message, CurrentUserOutputModel output)> GetCurrentUserByIdAsync(int userId);
    }
}
