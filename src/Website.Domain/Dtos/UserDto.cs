using System.ComponentModel.DataAnnotations;

namespace Website.Domain.Dtos
{
    public class UserSignUpInputDto 
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserSignInInputDto
    {
        public UserSignInInputDto(string password, string userName)
        {
            Password = password;
            UserName = userName;
        }

        [Required]
        public string UserName { get; }

        [Required]
        public string Password { get; }
    }

    public class UserChangePasswordInputDto
    {
        public UserChangePasswordInputDto(string newPassword, string oldPassword)
        {
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }

        [Required]
        public string OldPassword { get; }

        [Required]
        public string NewPassword { get; }
    }
    
    public class CurrentUserOutputDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Guid ExtensionId { get; set; }
    }

    public class UserSignInOutputDto
    {
        public string AccessToken { get; set; }

        public DateTime Expire { get; set; }
    }
}
