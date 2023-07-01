using System;
using Website.Shared.Entities;

namespace Website.Shared.Models
{
    public record UserSignUpInputModel
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public User MapToUserEntity()
        {
            var user = new User()
            {
                Surname = Surname,
                Name = Name,
                Email = Email,
                UserName = UserName,
                PhoneNumber = PhoneNumber,
            };
            user.SetPasswordHasher(Password);
            return user;
        }
    }
    
    public record UserSignInInputModel
    {
        public UserSignInInputModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public record UserChangePasswordInputModel
    {
        public UserChangePasswordInputModel(string newPassword, string oldPassword)
        {
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }

        public string OldPassword { get; }

        public string NewPassword { get; }
    }

    public record CurrentUserOutputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Guid ExtensionId { get; set; }
    }

    public record UserSignInOutputModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expire { get; set; }
    }
}
