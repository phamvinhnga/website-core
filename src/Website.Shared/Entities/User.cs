using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Entities
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(64)]
        public string Surname { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public Guid ExtensionId { get; set; } = Guid.NewGuid();

        public virtual string FullName => $"{this.Surname.Trim()} {this.Name.Trim()}";

        public void SetPasswordHasher(string password)
        {
            PasswordHash = new PasswordHasher<User>().HashPassword(this, password);
        }
    }
}
