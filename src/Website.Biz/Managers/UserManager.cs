using AutoMapper;
using Website.Biz.Managers.Interfaces;
using Website.Entity.Model;
using Website.Entity.Entities;
using Website.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Website.Entity.Repositories.Interfaces;
using Website.Shared.Extensions;

namespace Website.Biz.Managers
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRepository _userRepository;

        public UserManager(
             UserManager<User> userManager,
             RoleManager<Role> roleManager,
             IMapper mapper,
             IUserRepository userRepository
            ) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<StaffOutputModel>> GetListStaffAsync()
        {
            var query = await _userRepository.GetListUserStaff().ToListAsync();
            return _mapper.Map<List<StaffOutputModel>>(query);
        }

        public async Task RegisterStaffAsync(StaffRegisterInputModel input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if(await _userManager.FindByNameAsync(input.UserName) != null)
            {
                throw new BadRequestException($"UserName {input.UserName} already exists", StatusCodes.Status409Conflict);
            }

            var user = _mapper.Map<User>(input);
            user.SetPasswordHasher(input.Password);

            var resultUser = await _userManager.CreateAsync(user);

            if (!resultUser.Succeeded)
            {
                throw new BadRequestException(resultUser.Succeeded.ToString());
            }

            var resultRole = await _userManager.AddToRoleAsync(user, RoleExtension.Staff);

            if (!resultRole.Succeeded)
            {
                throw new BadRequestException(resultUser.Succeeded.ToString());
            }
        }
    }
}
